using dvdrental.DTOs.RequestDtos;
using dvdrental.DTOs.ResponceDtos;
using dvdrental.Entity;

namespace dvdrental.IService
{
    public interface IAdminService
    {
        Task<AdminResponceDto> CreateAdminAsync(AdminRequestDto requestDto);
        Task<List<AdminResponceDto>> GetAllMovies();
    }
}
