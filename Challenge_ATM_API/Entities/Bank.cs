﻿namespace Challenge_ATM_API.Entities
{
    public class Bank : BaseEntity
    {
        public required string Name { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public required string Address { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}