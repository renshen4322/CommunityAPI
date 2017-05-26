using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Core.Data;

namespace Communiry.Entity.Group
{
   public class GroupClassifyEntity:BaseEntity
    {
       public int Id { get; set; }
       public string Name { get; set; }

       public string Description { get; set; }

       public int Order { get; set; }
       public bool IsOffLine { get; set; }

       public DateTime GMTCreate { get; set; }

       public DateTime? GMTModified { get; set; }


    }
}
