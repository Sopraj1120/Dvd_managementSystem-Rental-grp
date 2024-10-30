namespace dvdrental.DTOs.ResponceDtos
{
    public class MovieResponseDto : MovieDto
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
