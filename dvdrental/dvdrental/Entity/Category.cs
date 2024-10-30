namespace dvdrental.Entity
{
    public class Category
    {
        public Guid Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public ICollection<Movies> Movies { get; set; }
    }

}
