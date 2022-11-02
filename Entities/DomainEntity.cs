namespace Entities
{
    public class DomainEntity<T>
    {
        public T Id { get; set; }
        public DomainEntity(T id)
        {
            Id = id;
        }

        //true if domain has an identity
        public bool IsTransient()
        {
            return Id.Equals(default(T));
        }
    }
}
