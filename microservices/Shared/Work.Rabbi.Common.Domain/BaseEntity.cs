using System.ComponentModel.DataAnnotations;

namespace Worl.Rabbi.Common.Domain
{
    public abstract class BaseEntity<TKey> : IHasKey<TKey>
    {
        [Key]
        public TKey Id { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}