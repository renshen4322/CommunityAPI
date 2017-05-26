using Community.Core.Data;
using System;

namespace Communiry.Entity
{
    public class DesignerMetaEntity : BaseEntity
    {
       public int Id { get; set; }
       public Guid BaseUserId { get; set; }
       public int DesignAge { get; set; }
       public DateTime CreatedDate { get; set; }
    }
}
