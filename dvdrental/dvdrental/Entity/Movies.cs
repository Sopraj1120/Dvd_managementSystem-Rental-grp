namespace dvdrental.Entity
{
    public class Movies
    {
        public Guid Id { get; set; }


        public string Title { get; set; }

        public string Description { get; set; }


        public int Copies { get; set; }


        public Guid CategoryId { get; set; }


        public string CategoryName { get; set; }

        public string Image { get; set; }

        public bool IsDeleted { get; set; }
    }
}
