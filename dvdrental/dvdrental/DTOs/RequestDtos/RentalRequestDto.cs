using static dvdrental.Entity.RentalRequest;

namespace dvdrental.DTOs
{
    public class RentalRequestDto
    {
        public Guid Id { get; set; }
        public Guid MovieId { get; set; }

        public string MovieTitle { get; set; }
        public string MovieImage { get; set; }
        public int AvailableCopies { get; set; }

        public Guid CustomerId { get; set; }

        public string CustomerName { get; set; }

        public RentalStatus Status { get; set; } = RentalStatus.Pending;

        public string RentDate { get; set; }
       


    }
}
