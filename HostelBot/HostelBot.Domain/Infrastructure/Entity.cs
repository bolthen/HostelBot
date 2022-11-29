using System.Collections.Generic;

namespace HostelBot.Domain.Infrastructure
{
    public class Entity<TId, TType> : DddObject<TType>
        where TType : Entity<TId, TType>
    {
        public TId Id { get; }
        
        public Entity(TId id)
        {
            Id = id;
        }

        protected bool Equals(Entity<TId, TType> other)
        {
            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Entity<TId, TType>)obj);
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