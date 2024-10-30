using dvdrental.DTOs;
using dvdrental.DTOs.ResponceDtos;
using dvdrental.Entity;
using dvdrental.IRepository;
using dvdrental.IService;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace dvdrental.Service
{
    public class RentalRequestService : IRentalRequestService
    {
        private readonly IRentalRequestRepository _rentalRequestRepository;

        public RentalRequestService(IRentalRequestRepository rentalRequestRepository)
        {
            _rentalRequestRepository = rentalRequestRepository;
        }

        public async Task<RentalResponceDto> AddRental(RentalRequest responseDto)
        {
            
            var rentalRequest = new RentalRequest
            {
                Id = Guid.NewGuid(), 
                MovieId = responseDto.MovieId,
                MovieTitle = responseDto.MovieTitle,
                MovieImage = responseDto.MovieImage,
                AvailableCopies = responseDto.AvailableCopies,
                CustomerId = responseDto.CustomerId,
                Status = responseDto.Status,
                RentDate = DateTime.UtcNow.ToString("o"), 
                ReturnDate = DateTime.UtcNow.AddDays(7).ToString("o") 
            };

            
            var addedRental = await _rentalRequestRepository.AddRentalRequest(rentalRequest);

            
            return new RentalResponceDto
            {
                Id = addedRental.Id,
                MovieId = addedRental.MovieId,
                MovieTitle = addedRental.MovieTitle,
                MovieImage = addedRental.MovieImage,
                AvailableCopies = addedRental.AvailableCopies ?? 0,
                CustomerId = addedRental.CustomerId,
                CustomerName = responseDto.CustomerName, 
                Status = addedRental.Status,
                RentDate = addedRental.RentDate,
                ReturnDate = addedRental.ReturnDate
            };
        }

        public async Task<IEnumerable<RentalResponceDto>> GetAllRentals()
        {
            
            var rentalRequests = await _rentalRequestRepository.GetAllRentals();

            
            var responseDtos = rentalRequests.Select(rental => new RentalResponceDto
            {
                Id = rental.Id,
                MovieId = rental.MovieId,
                MovieTitle = rental.MovieTitle,
                MovieImage = rental.MovieImage,
                AvailableCopies = rental.AvailableCopies ?? 0, 
                CustomerId = rental.CustomerId,
                Status = rental.Status,
                RentDate = rental.RentDate,
                ReturnDate = rental.ReturnDate
            });

            return responseDtos.ToList(); 
        }



        public async Task<RentalResponceDto> GetRentalById(Guid id)
        {
            
            var rentalRequest = await _rentalRequestRepository.GetById(id); 
            if (rentalRequest == null)
            {
                throw new KeyNotFoundException("Rental request not found"); 
            }

            
            return new RentalResponceDto
            {
                Id = rentalRequest.Id,
                MovieId = rentalRequest.MovieId,
                MovieTitle = rentalRequest.MovieTitle,
                MovieImage = rentalRequest.MovieImage,
                AvailableCopies = rentalRequest.AvailableCopies ?? 0, 
                CustomerId = rentalRequest.CustomerId,
                Status = rentalRequest.Status,
                RentDate = rentalRequest.RentDate,
                ReturnDate = rentalRequest.ReturnDate
            };
        }

        public async Task<RentalResponceDto> UpdateRental(RentalRequestDto dto)
        {
            var existingRental = await _rentalRequestRepository.GetById(dto.Id); 

            if (existingRental == null)
            {
                throw new KeyNotFoundException("Rental request not found");
            }

            existingRental.MovieId = dto.MovieId;
            existingRental.MovieTitle = dto.MovieTitle;
            existingRental.MovieImage = dto.MovieImage;
            existingRental.AvailableCopies = dto.AvailableCopies;
            existingRental.CustomerId = dto.CustomerId;
            existingRental.Status = dto.Status;
            existingRental.RentDate = dto.RentDate;

            var updatedRental = await _rentalRequestRepository.UpdateRentals(existingRental);

            return new RentalResponceDto
            {
                Id = updatedRental.Id,
                MovieId = updatedRental.MovieId,
                MovieTitle = updatedRental.MovieTitle,
                MovieImage = updatedRental.MovieImage,
                AvailableCopies = updatedRental.AvailableCopies ?? 0,
                CustomerId = updatedRental.CustomerId,
                Status = updatedRental.Status,
                RentDate = updatedRental.RentDate,
                ReturnDate = updatedRental.ReturnDate
            };
        }

        public async Task<IEnumerable<RentalResponceDto>> GetRentalByCustomerId(Guid customerId)
        {
            var rentalRequests = await _rentalRequestRepository.GetByCustomerId(customerId);

            return rentalRequests.Select(rental => new RentalResponceDto
            {
                Id = rental.Id,
                MovieId = rental.MovieId,
                MovieTitle = rental.MovieTitle,
                MovieImage = rental.MovieImage,
                AvailableCopies = rental.AvailableCopies ?? 0,
                CustomerId = rental.CustomerId,
                Status = rental.Status,
                RentDate = rental.RentDate,
                ReturnDate = rental.ReturnDate
            }).ToList();
        }



    }
}
