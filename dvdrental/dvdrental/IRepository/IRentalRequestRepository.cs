using dvdrental.DTOs.ResponceDtos;
using dvdrental.Entity;

namespace dvdrental.IRepository
{

    public interface IRentalRequestRepository
    {
        Task<RentalRequest> AddRentalRequest(RentalRequest rental);
        Task<IEnumerable<RentalRequest>> GetAllRentals();
        Task<RentalRequest> UpdateRentals(RentalRequest request);
        Task<RentalRequest> GetById(Guid id);
      Task<IEnumerable<RentalRequest>> GetByCustomerId(Guid customerId);



    }

}
