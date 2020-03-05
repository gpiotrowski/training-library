namespace Library.Orders.Services.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BookLimit { get; set; } = 2;
    }
}