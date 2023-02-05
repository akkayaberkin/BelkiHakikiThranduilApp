namespace BelkiHakiki.Core
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public Guid? Guid { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
