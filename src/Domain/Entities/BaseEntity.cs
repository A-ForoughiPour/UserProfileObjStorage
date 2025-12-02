using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get;protected set; }
        public DateTime CreatedAt { get;protected set; }
        public DateTime UpdatedAt { get;protected set; }
        public DateTime DeletedAt { get;protected set; }
        public bool IsDeleted { get;protected set; }
        protected BaseEntity()
        {
            this.CreatedAt = DateTime.Now;
            this.IsDeleted = false;
        }
        public void Remove()
        {
            this.IsDeleted = true;
            this.DeletedAt = DateTime.Now;
        }
    }
}
