using Community.Core.Data;
using System;

namespace Communiry.Entity
{
    public class ProductMetaEntity : BaseEntity
    {
       public int Id { get; set; }
       public Guid PruductId { get; set; }
        public bool IsHot { get; set; }

    }
}
