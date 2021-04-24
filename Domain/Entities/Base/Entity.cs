using System;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Domain.Entities.Base
{
    public abstract class Entity
    {
        // ReSharper disable once UnusedMember.Global
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; }
    }
}