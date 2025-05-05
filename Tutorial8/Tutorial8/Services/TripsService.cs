using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Tutorial8.Models.DTOs;

namespace Tutorial8.Services;

public class TripsService : ITripsService
{
    public readonly string _connectionString = "Server=DESKTOP-SRO51HP\\MSSQLSERVER04;Database=master;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True;";
    
    public async Task<List<TripDTO>> GetTrips()
    {
        var trips = new List<TripDTO>();

        using (SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(@"
                SELECT t.IdTrip, t.Name, t.Description, t.DateFrom, t.DateTo, t.MaxPeople, STRING_AGG(c.Name, ', ') AS Countries 
                From Trip t JOIN Country_Trip ct ON t.IdTrip = ct.IdTrip JOIN Country c ON ct.IdCountry = c.IdCountry 
                GROUP BY t.IdTrip, t.Name, t.Description, t.DateFrom, t.DateTo, t.MaxPeople", conn))
        {
            await conn.OpenAsync();

            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    int idOrdinal = reader.GetOrdinal("IdTrip");
                    trips.Add(new TripDTO()
                    {
                        Id = reader.GetInt32(idOrdinal),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        DateFrom = reader.GetDateTime(3),
                        DateTo = reader.GetDateTime(4),
                        MaxPeople = reader.GetInt32(5),
                        Countries = (string)reader["Countries"]
                    });
                }
            }
        }

        return trips;
    }

    public async Task<IActionResult> GetTrip(int id)
    { 
        var trips = new List<TripDTO>();
        
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        using var check = new SqlCommand("SELECT 1 FROM Client WHERE IdClient = @id", conn);
        check.Parameters.AddWithValue("@id", id);
        var exists = await check.ExecuteScalarAsync();
        if (exists == null)
        {
            return new NotFoundObjectResult("Client not found");
        }

        using var command = new SqlCommand(@"
                SELECT t.IdTrip, t.Name, t.Description, t.DateFrom, t.DateTo, t.MaxPeople, ct.RegisteredAt, ct.PaymentDate
                FROM Trip t
                JOIN Client_Trip ct ON t.IdTrip = ct.IdTrip
                WHERE ct.IdClient = @id", conn);
        command.Parameters.AddWithValue("@id", id);

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {

            int idOrdinal = reader.GetOrdinal("IdTrip");
            trips.Add(new TripDTO()
            {
                Id = reader.GetInt32(idOrdinal),
                Name = reader.GetString(1),
                Description = reader.GetString(2),
                DateFrom = reader.GetDateTime(3),
                DateTo = reader.GetDateTime(4),
                MaxPeople = reader.GetInt32(5),

            });
        }

        return new OkObjectResult(new { Trips = trips });
    }

    public async Task<IActionResult> CreateClient(ClientDTO client)
    {
        if (string.IsNullOrWhiteSpace(client.FirstName) ||
            string.IsNullOrWhiteSpace(client.LastName) ||
            string.IsNullOrWhiteSpace(client.Email) ||
            string.IsNullOrWhiteSpace(client.Telephone) ||
            string.IsNullOrWhiteSpace(client.Pesel))
        { 
            return new BadRequestObjectResult("You must provide all fields");   
        }

        string command =
            " INSERT INTO Client (FirstName, LastName, Email, Telephone, Pesel) OUTPUT INSERTED.IdClient VALUES (@FirstName, @LastName, @Email, @Telephone, @Pesel)";
        using var conn = new SqlConnection(_connectionString);
        using (SqlCommand cmd = new SqlCommand(command, conn)){
            await conn.OpenAsync(); 
        cmd.Parameters.AddWithValue("@FirstName", client.FirstName);
        cmd.Parameters.AddWithValue("@LastName", client.LastName);
        cmd.Parameters.AddWithValue("@Email", client.Email);
        cmd.Parameters.AddWithValue("@Telephone", client.Telephone);
        cmd.Parameters.AddWithValue("@Pesel", client.Pesel);

        var insertedId = (int)await cmd.ExecuteScalarAsync();
        return new OkObjectResult(new { IdClient = insertedId });
    }
    }

    public async Task<IActionResult> AddClientToTrip(int id_client, int id_trip)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        using var checkClient = new SqlCommand("SELECT 1 FROM Client WHERE IdClient = @id", conn);
        checkClient.Parameters.AddWithValue("@id", id_client);
        if (await checkClient.ExecuteScalarAsync() == null)
            return new NotFoundObjectResult("Client not found");

        using var checkTrip = new SqlCommand("SELECT MaxPeople FROM Trip WHERE IdTrip = @tripId", conn);
        checkTrip.Parameters.AddWithValue("@tripId", id_trip);
        var maxPeople = await checkTrip.ExecuteScalarAsync();
        if (maxPeople == null)
            return new NotFoundObjectResult("Trip not found");

        using var count = new SqlCommand("SELECT COUNT(*) FROM Client_Trip WHERE IdTrip = @tripId", conn);
        count.Parameters.AddWithValue("@tripId", id_trip);
        var currentCount = (int)await count.ExecuteScalarAsync();
        if (currentCount >= (int)maxPeople)
            return new BadRequestObjectResult("Trip is full");

        using var insert = new SqlCommand("INSERT INTO Client_Trip VALUES (@id, @tripId, @date, NULL)", conn);
        insert.Parameters.AddWithValue("@id", id_client);
        insert.Parameters.AddWithValue("@tripId", id_trip);
        insert.Parameters.AddWithValue("@date", 123);
        await insert.ExecuteNonQueryAsync();

        return new OkObjectResult("Client registered to trip");
    }

    public async Task<IActionResult> DeleteClientFromTrip(int id_client, int id_trip)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        using var check = new SqlCommand("SELECT 1 FROM Client_Trip WHERE IdClient = @id AND IdTrip = @tripId", conn);
        check.Parameters.AddWithValue("@id", id_client);
        check.Parameters.AddWithValue("@tripId", id_trip);
        if (await check.ExecuteScalarAsync() == null)
            return new NotFoundObjectResult("Client on trip not found");

        using var delete = new SqlCommand("DELETE FROM Client_Trip WHERE IdClient = @id AND IdTrip = @tripId", conn);
        delete.Parameters.AddWithValue("@id", id_client);
        delete.Parameters.AddWithValue("@tripId", id_trip);
        await delete.ExecuteNonQueryAsync();

        return new OkObjectResult("Client unregistered from trip");
        }
    }
