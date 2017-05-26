using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Model
{

   public class AllResourceModel
    {
       public string ResourceType { get; set; }
       public Guid ResourceId { get; set; }
       public string Title { get; set; }
       public string Intro { get; set; }
       public string Thumbnail { get; set; }
       public string ResourceUrl { get; set; }
       public DateTime CreatedDate { get; set; }
   }
}
