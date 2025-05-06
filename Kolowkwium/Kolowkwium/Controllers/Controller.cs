using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Kolowkwium.Models.DTOs;
using Kolowkwium.Services;

namespace Kolowkwium.Controllers
{
    [Route("api")]
    [ApiController]
    public class Controller : ControllerBase
    {
        private readonly Service _Service;

        public Controller(Service Service)
        {
            _Service = Service;
        }

        [HttpGet("trips")]
        public async Task<IActionResult> GetTrips()
        {
            var trips = await _tripsService.GetTrips();
            return Ok(trips);
        }

        [HttpGet("clients/{id}/trips")]
        public async Task<IActionResult> GetTrip(int id)
        {
            var trips = await _tripsService.GetTrip(id);
            return Ok(trips);
        }

        [HttpPost("clients")]
        public async Task<IActionResult> CreateClient([FromBody] ClientDTO dto)
        {
            var id = await _tripsService.CreateClient(dto);
            return Ok(id);
        }

        [HttpPut("clients/{id}/trips/{tripId}")]
        public async Task<IActionResult> RegisterClientToTrip(int id, int tripId)
        {
            var result = await _tripsService.AddClientToTrip(id, tripId);
            return Ok(result);
        }

        [HttpDelete("clients/{id}/trips/{tripId}")]
        public async Task<IActionResult> UnregisterClientFromTrip(int id, int tripId)
        {
            var result = await _tripsService.DeleteClientFromTrip(id, tripId);
            return Ok(result);
        }


    }
    
}