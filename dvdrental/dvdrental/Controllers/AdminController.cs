using dvdrental.DTOs.RequestDtos;
using dvdrental.DTOs.ResponceDtos;
using dvdrental.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dvdrental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminRequestDto requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest("Admin data is required.");
            }

            try
            {
                var adminResponse = await _adminService.CreateAdminAsync(requestDto);
                return CreatedAtAction(nameof(CreateAdmin), new { id = adminResponse.Id }, adminResponse);
            }
            catch (ArgumentException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating admin: {ex.Message}");
                return StatusCode(500, "An unexpected error occurred while creating the admin.");
            }
        }

        [HttpGet("GetCustomer")]
        public async Task<IActionResult> GetAllMovies()
        {
            try
            {
                var data = await _adminService.GetAllMovies();
                return Ok(data);
            }
            catch (ArgumentException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating admin: {ex.Message}");
                return StatusCode(500, "An unexpected error occurred while creating the admin.");
            }
           

        }
    }
}
