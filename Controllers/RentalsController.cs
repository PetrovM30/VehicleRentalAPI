using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRentalAPI.Context;
using VehicleRentalAPI.DTO;
using VehicleRentalAPI.Entities;

namespace rentalRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController(VehicleRentalContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rentals = await context.Rentals.ToListAsync();
            if (rentals is null) return BadRequest("nem sikerült a lekérdezés");

            return Ok(rentals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rental = await context.Rentals.SingleOrDefaultAsync(x => x.Id == id);
            if (rental is null) return BadRequest("nem sikerült a lekérdezés");

            return Ok(rental);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RentalDTO rentalDTO)
        {
            var rental = new Rental
            {
                CustomerId = rentalDTO.CustomerId,
                VehicleId = rentalDTO.VehicleId,
                RetalDate = rentalDTO.RetalDate,
                ReturnDate = rentalDTO.ReturnDate,
            };
            if (rental is null) return BadRequest("hiba");

            context.Rentals.Add(rental);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = rental.Id }, rental);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Rental rental)
        {
            if (rental is null) return BadRequest("eh");

            var rentalToUpdate = await context.Rentals.SingleOrDefaultAsync(x => x.Id == id);
            if (rentalToUpdate is null) return BadRequest("hiba történt a módosítás során");

            rentalToUpdate.CustomerId = rental.CustomerId;
            rentalToUpdate.VehicleId = rental.VehicleId;
            rentalToUpdate.RetalDate = rental.RetalDate;
            rentalToUpdate.ReturnDate = rental.ReturnDate;
            await context.SaveChangesAsync();
            return Ok("sikeres módosítás");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rentalToDelete = await context.Rentals.SingleOrDefaultAsync(x => x.Id == id);
            if (rentalToDelete is null) return BadRequest("valami hiba történt");

            context.Rentals.Remove(rentalToDelete);
            await context.SaveChangesAsync();
            return Ok("A törlés sikeres volt");
        }


    }
}
