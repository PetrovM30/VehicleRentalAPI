using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRentalAPI.Context;
using VehicleRentalAPI.Entities;

namespace VehicleRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(VehicleRentalContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await context.Customers.ToListAsync();
            if (customers is null) return BadRequest("nem sikerült a lekérdezés");

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await context.Customers.SingleOrDefaultAsync(x => x.Id == id);
            if (customer is null) return BadRequest("nem sikerült a lekérdezés");

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            if (customer is null) return BadRequest("hiba");

            context.Customers.Add(customer);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new {id = customer.Id}, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Customer customer)
        {
            if (customer is null) return BadRequest("eh");
            
            var customerToUpdate = await context.Customers.SingleOrDefaultAsync(x => x.Id == id);
            if (customerToUpdate is null) return BadRequest("hiba történt a módosítás során");

            customerToUpdate.Name = customer.Name;
            customerToUpdate.Email = customer.Email;
            customerToUpdate.PhoneNumber = customer.PhoneNumber;
            await context.SaveChangesAsync();
            return Ok("sikeres módosítás");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customerToDelete = await context.Customers.SingleOrDefaultAsync(x =>x.Id == id);
            if (customerToDelete is null) return BadRequest("valami hiba történt");

            context.Customers.Remove(customerToDelete);
            await context.SaveChangesAsync();
            return Ok("A törlés sikeres volt");
        }
    }
}
