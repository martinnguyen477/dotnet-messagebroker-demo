﻿using Common.Examples;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            CustomerService = customerService;
            Logger = logger;
        }

        [HttpGet]
        public Task<List<Customer>> Get()
        {
            return Task.FromResult(new List<Customer>());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerRequestExample(typeof(CustomerExample), typeof(CustomerExample))]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            await CustomerService.CreateCustomer(customer);
            return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
        }

        private readonly ICustomerService CustomerService;
        private readonly ILogger<CustomerController> Logger;
    }
}