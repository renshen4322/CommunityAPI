using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Model.Group
{
   public class MemberInfoModel
    {
       public Guid Id { get; set; }
       public string Role { get; set; }
       public string Bio { get; set; }
       public string Name { get; set; }
       public string AvatarUrl { get; set; }
       public DateTime GMTCreate { get; set; }
    }
}
