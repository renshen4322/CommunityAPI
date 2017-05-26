using Community.Core.Data;
using System;

namespace Communiry.Entity
{
    public class CategoryRelationshipsEntity : BaseEntity
    {
      public int Id { get; set; }
      public int CategoryId { get; set; }
      public Guid ObjectId { get; set; }
      public DateTime CreatedDate { get; set; }
    }
}
