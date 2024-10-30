namespace dvdrental.DTOs.ResponceDtos
{
    public class CustomerResponseDto : CustomerDto
    {
        public Guid Id { get; set; }
        public string IsActive { get; set; }
    }

}
