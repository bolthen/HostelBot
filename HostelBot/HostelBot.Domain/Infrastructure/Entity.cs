using System.Collections.Generic;

namespace HostelBot.Domain.Infrastructure
{
    public class Entity<TType, TId> : DddObject<TType>
        where TType : Entity<TType, TId>
    {
        public TId Id { get; set; }

        protected bool Equals(Entity<TType, TId> other)
        {
            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Entity<TType, TId>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TId>.Default.GetHashCode(Id);
        }

        public override string ToString()
        {
            return $"{GetType().Name}({nameof(Id)}: {Id})";
        }
    }
}