using dvdrental.DTOs.RequestDtos;
using dvdrental.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dvdrental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
       

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
           
        }

    
        [HttpPost]
        [Route("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerRequestDto customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer data is required!");
            }

            try
            {
                var createdCustomer = await _customerService.AddCustomer(customer);
                return CreatedAtAction(nameof(CreateCustomer), new { id = createdCustomer.Id }, createdCustomer);
            }
            catch (ArgumentException ex)
            {
               
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
             
                return StatusCode(500, "An unexpected error occurred while creating the customer.");
            }
        }

        [HttpGet]
        [Route("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllCustomers();
                return Ok(customers);
            }
            catch (Exception )
            {
                
                return StatusCode(500, "An unexpected error occurred while retrieving customers.");
            }
        }

      
        [HttpGet]
        [Route("GetCustomerById/{id}")]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            try
            {
                var customer = await _customerService.GetCustomerById(id);
                if (customer == null)
                {
                    return NotFound($"Customer with ID {id} not found.");
                }
                return Ok(customer);
            }
            catch (Exception)
            {
               
                return StatusCode(500, $"An unexpected error occurred while retrieving customer with ID {id}.");
            }
        }

       
        [HttpPut]
        [Route("UpdateCustomer/{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] CustomerRequestDto customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer data is required!");
            }

            try
            {
                var updatedCustomer = await _customerService.UpdateCustomer(id, customer);
                return Ok(updatedCustomer);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
               
                return NotFound(ex.Message);
            }
            catch (Exception )
            {
              
                return StatusCode(500, $"An unexpected error occurred while updating customer with ID {id}.");
            }
        }


        [HttpDelete]
        [Route("DeleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            try
            {
                var deleted = await _customerService.DeleteCustomer(id);
                if (!deleted)
                {
                    return NotFound($"Customer with ID {id} not found.");
                }
                return NoContent();
            }
            catch (Exception)
            {
               
                return StatusCode(500, $"An unexpected error occurred while deleting customer with ID {id}.");
            }
        }
    }
}
