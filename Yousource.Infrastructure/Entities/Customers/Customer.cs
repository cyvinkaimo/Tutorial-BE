namespace Yousource.Infrastructure.Entities.Customers
{
    using System;

    public class Customer : Entity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
