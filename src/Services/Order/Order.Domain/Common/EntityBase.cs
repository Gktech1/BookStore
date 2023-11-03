

namespace Domain.Common
{
    public abstract class EntityBase
    {
        public int OrderId { get; protected set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
