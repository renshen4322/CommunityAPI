using Community.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communiry.Entity
{
    public class TagRelationshipsEntity : BaseEntity
    {
       public int Id { get; set; }
       public int TagId { get; set; }
       public Guid ObjectId { get; set; }
       public DateTime CreatedDate { get; set; }
    }
}
