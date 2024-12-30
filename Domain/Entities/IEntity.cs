namespace StreetService.Domain.Entities
{
    public interface IEntity<TKey> where TKey : notnull
    {
        public TKey Id { get; }
        public DateTime CreatedDate { get; }
        public bool IsDeleted { get; set; }
    }
}
