using Microsoft.AspNetCore.Mvc;
using Tutorial8.Models.DTOs;

namespace Tutorial8.Services;

public interface ITripsService
{
    Task<List<TripDTO>> GetTrips();
    Task<IActionResult> GetTrip(int id);
    Task<IActionResult> CreateClient(ClientDTO client);
    Task<IActionResult> AddClientToTrip(int id_client, int id_trip);
    Task<IActionResult> DeleteClientFromTrip(int id_client, int id_trip);
    
}