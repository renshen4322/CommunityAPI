using Community.Core.Data;
using System;

namespace Communiry.Entity
{
    public class SupplierMetaEntity : BaseEntity
    {
       public int Id { get; set; }
       public Guid BaseUserId { get; set; }
       public string Moblie { get; set; }
       public DateTime CreatedDate { get; set; }
    }
}
