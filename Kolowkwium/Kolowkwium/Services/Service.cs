using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Kolowkwium.Models.DTOs;

namespace Kolowkwium.Services;

public class Service : IService
{
    public readonly string _connectionString = "Server=DESKTOP-SRO51HP\\MSSQLSERVER04;Database=master;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True;";
    
}