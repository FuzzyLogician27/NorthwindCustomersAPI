using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindCustomersAPI.Models;
using NorthwindCustomersAPI.Models.DTO;
using NorthwindCustomersAPI.Services;

namespace NorthwindCustomersAPI.Controllers
{
    [Route("api/Customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly INorthwindService _service;

        public CustomersController(INorthwindService service)
        {
            _service = service;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            var customers = await _service.GetAllAsync();
          if (customers is null)
          {
              return NotFound();
          }
            return customers.Select(c=>Utils.CustomerToDTO(c)).ToList();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(string id)
        {
            var customer = await _service.GetAsync(id);
          if (customer is null)
          {
              return NotFound();
          }
            return Utils.CustomerToDTO(customer);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(string id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            


            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer([Bind("CustomerId,CompanyName,ContactName,ContactTitle,City,Region,Country,Fax")]Customer customer)
        {
            var customerCreated = await _service.CreateAsync(customer);
            if (customerCreated)
            {
                return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
            }
            return Problem("Customer not created");

        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var isDeleted = await _service.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
