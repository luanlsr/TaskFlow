namespace TaskFlow.Domain.Core.Entities
{
    public abstract class EntityBase<TId>
    {
        public TId Id { get; protected set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool? Status { get; set; }

        protected EntityBase() { }

        protected EntityBase(TId id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if (obj is not EntityBase<TId> other) return false;
            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TId>.Default.GetHashCode(Id);
        }
    }
}
