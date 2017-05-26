using Community.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communiry.Entity
{
    public class WorksMetaEntity : BaseEntity
    {
       public int Id { get; set; }
       public Guid WorksId { get; set; }
       public float? Cost { get; set; }
   
       public float ActualArea { get; set; }    
         
       public string helper { get; set; }   
        public bool IsHot { get; set; }
    }
}
