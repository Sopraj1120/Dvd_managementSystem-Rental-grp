using System.ComponentModel.DataAnnotations;

namespace dvdrental.DTOs
{
    public class MovieDto
    {
        [Required(ErrorMessage = "The Title field is required.")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "The Copies field is required.")]
        public int Copies { get; set; }

        [Required(ErrorMessage = "The CategoryId field is required.")]
        public Guid CategoryId { get; set; }

        // Add [Required] to CategoryName
        [Required(ErrorMessage = "The CategoryName field is required.")]
        public string CategoryName { get; set; }

        public string Image { get; set; }


    }
}
