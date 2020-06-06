namespace Yousource.Services.Customer.Data.Entities
{
    using System;

    public class CustomerEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
