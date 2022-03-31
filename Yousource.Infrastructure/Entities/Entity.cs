namespace Yousource.Infrastructure.Entities
{
    using System;

    public abstract class Entity
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid UpdatedBy { get; set; }
    }
}
