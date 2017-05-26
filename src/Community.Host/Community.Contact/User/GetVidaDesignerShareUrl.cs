using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.User
{
   public class GetVidaDesignerShareUrl:CommandRequestDto
    {
       //资源id
       public string object_id { get; set; }
    }
   public class GetVidaDesignerShareUrlResponse : CommandRequestDto
   {
       /// <summary>
       /// 分享url
       /// </summary>
       public string share_url { get; set; }
   }
}
