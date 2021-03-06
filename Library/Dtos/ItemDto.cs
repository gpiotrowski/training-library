﻿namespace Library.Dtos
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public int AvailableQuantity { get; set; }
    }
}