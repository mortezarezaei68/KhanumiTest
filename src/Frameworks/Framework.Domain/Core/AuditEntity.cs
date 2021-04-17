using System;

namespace Framework.Domain.Core
{
    public abstract class EntityAudit
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}