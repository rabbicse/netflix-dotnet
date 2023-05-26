namespace Worl.Rabbi.Common.Domain
{
    public abstract class AuditableEntity<TKey> : BaseEntity<TKey>
    {
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; protected set; } = DateTime.UtcNow;
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; protected set; }
    }
}