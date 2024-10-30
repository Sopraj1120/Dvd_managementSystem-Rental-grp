using dvdrental.DTOs;
using dvdrental.DTOs.RequestDtos;
using dvdrental.DTOs.ResponceDtos;
using dvdrental.Entity;

namespace dvdrental.IService
{
    public interface IRentalRequestService
    {
        Task<RentalResponceDto> AddRental(RentalRequest responseDto);
        Task<IEnumerable<RentalResponceDto>> GetAllRentals();
        Task<RentalResponceDto> GetRentalById(Guid id);
        Task<RentalResponceDto> UpdateRental(RentalRequestDto dto);
        Task<IEnumerable<RentalResponceDto>> GetRentalByCustomerId(Guid customerId);

    }
}
