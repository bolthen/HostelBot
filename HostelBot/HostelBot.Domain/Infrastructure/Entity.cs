using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HostelBot.Domain.Infrastructure
{
    public class Entity<TType> : DddObject<TType>
        where TType : Entity<TType>
    {
        [Key]
        public long Id { get; set; }

        protected bool Equals(Entity<TType> other)
        {
            return EqualityComparer<long>.Default.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Entity<TType>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<long>.Default.GetHashCode(Id);
        }

        public override string ToString()
        {
            return $"{GetType().Name}({nameof(Id)}: {Id})";
        }
    }
}