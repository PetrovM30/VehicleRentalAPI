using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRentalAPI.Context;
using VehicleRentalAPI.Entities;

namespace VehicleRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController(VehicleRentalContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var vehicles = await context.Vehicles.ToListAsync();
            if (vehicles is null) return BadRequest("nem sikerült a lekérdezés");

            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vehicle = await context.Vehicles.SingleOrDefaultAsync(x => x.Id == id);
            if (vehicle is null) return BadRequest("nem sikerült a lekérdezés");

            return Ok(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Vehicle vehicle)
        {
            if (vehicle is null) return BadRequest("hiba");

            context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = vehicle.Id }, vehicle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Vehicle vehicle)
        {
            if (vehicle is null) return BadRequest("eh");

            var vehicleToUpdate = await context.Vehicles.SingleOrDefaultAsync(x => x.Id == id);
            if (vehicleToUpdate is null) return BadRequest("hiba történt a módosítás során");

            vehicleToUpdate.Model = vehicle.Model;
            vehicleToUpdate.LicensePlate = vehicle.LicensePlate;
            vehicleToUpdate.DaliyRate = vehicle.DaliyRate;
            vehicleToUpdate.Available = vehicle.Available;
            await context.SaveChangesAsync();
            return Ok("sikeres módosítás");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var vehicleToDelete = await context.Vehicles.SingleOrDefaultAsync(x => x.Id == id);
            if (vehicleToDelete is null) return BadRequest("valami hiba történt");

            context.Vehicles.Remove(vehicleToDelete);
            await context.SaveChangesAsync();
            return Ok("A törlés sikeres volt");
        }
    }
}
