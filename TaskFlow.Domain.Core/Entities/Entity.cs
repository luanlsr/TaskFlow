namespace TaskFlow.Domain.Core.Entities
{
    public abstract class Entity<TId>
    {
        public TId Id { get; protected set; }

        protected Entity() { }

        protected Entity(TId id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Entity<TId> other) return false;
            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TId>.Default.GetHashCode(Id);
        }
    }
}
