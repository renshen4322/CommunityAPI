using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
   public class GetGroupDetialRequestDto:BaseRequestDto
    {
       /// <summary>
       /// 組id
       /// </summary>
       public int group_id { get; set; }
    }
   public class GetGroupDetialResponseDto : BaseResponseDto
   {
      
       public GroupDetailInfoDto data { get; set; }
   }
}
