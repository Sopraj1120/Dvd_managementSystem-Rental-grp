using dvdrental.Entity;

namespace dvdrental.IRepository
{
    public interface IAdminRepository
    {
        Task<List<Admin>> GetAllMovies();
        Task<Admin> AddAdminAsync(Admin admin);
    }
}
