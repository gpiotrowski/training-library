namespace Library.Leases.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public int AvailableQuantity { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
    }
}
