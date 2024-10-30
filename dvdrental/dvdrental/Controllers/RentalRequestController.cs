using Microsoft.AspNetCore.Mvc;
using dvdrental.DTOs.RequestDtos;
using dvdrental.DTOs.ResponceDtos;
using dvdrental.IService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dvdrental.DTOs;
using dvdrental.Entity;

namespace dvdrental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalRequestController : ControllerBase
    {
        private readonly IRentalRequestService _rentalRequestService;

        public RentalRequestController(IRentalRequestService rentalRequestService)
        {
            _rentalRequestService = rentalRequestService;
        }

        
        [HttpPost]
        public async Task<ActionResult<RentalResponceDto>> AddRental([FromBody] RentalRequest rentalRequest)
        {
            if (rentalRequest == null)
            {
                return BadRequest("Rental request cannot be null.");
            }

            var response = await _rentalRequestService.AddRental(rentalRequest);
            return CreatedAtAction(nameof(GetRentalById), new { id = response.Id }, response);
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalResponceDto>>> GetAllRentals()
        {
            var rentals = await _rentalRequestService.GetAllRentals();
            return Ok(rentals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RentalResponceDto>> GetRentalById(Guid id)
        {
            var rental = await _rentalRequestService.GetRentalById(id);
            if (rental == null)
            {
                return NotFound();
            }

            return Ok(rental);
        }

      
        [HttpPut]
        public async Task<ActionResult<RentalResponceDto>> UpdateRental([FromBody] RentalRequestDto rentalRequestDto)
        {
            if (rentalRequestDto == null)
            {
                return BadRequest("Rental request DTO cannot be null.");
            }

            var updatedRental = await _rentalRequestService.UpdateRental(rentalRequestDto);
            if (updatedRental == null)
            {
                return NotFound();
            }

            return Ok(updatedRental);
        }

        
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<RentalResponceDto>>> GetRentalByCustomerId(Guid customerId)
        {
            var rentals = await _rentalRequestService.GetRentalByCustomerId(customerId);
            return Ok(rentals);
        }
    }
}
