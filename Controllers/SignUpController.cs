using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnBoarding.Models;
using OnBoarding.Services;

namespace OnBoarding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private ICredentialsService _service;
        public SignupController(ICredentialsService service)
        {
            _service = service;
        }
        // GET: api/Customer_Signup
        [HttpGet]
        public IEnumerable<Customer> GetCustomer_Signup()
        {
            return _service.GetAllSignUp();
        }
        // GET: api/Customer_Signup/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer_Signup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Customer customer_Signup = await _service.GetSignUp(id);
            if (customer_Signup == null)
            {
                return NotFound();
            }
            return Ok(customer_Signup);
        }

        [HttpGet("query")]
        public async Task<IActionResult> GetCustomerByQuery([FromQuery(Name = "Customer_name")] string Customer_name, [FromQuery(Name = "Email")] string Email)
        {
            var result = _service.GetAllCustomer(Customer_name, Email);
            return Ok(result);
        }

        // POST: api/Customer_Signup
        [HttpPost]
        public async Task<IActionResult> PostCustomer_Signup([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _service.CreateCredentials(customer);
            return CreatedAtAction("GetCustomer_Signup", new { id = customer.Id }, customer);
        }
    }
}