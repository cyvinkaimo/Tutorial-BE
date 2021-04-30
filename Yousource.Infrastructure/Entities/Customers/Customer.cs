namespace Yousource.Infrastructure.Entities.Customers
{
    using System;

    public class Customer
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
