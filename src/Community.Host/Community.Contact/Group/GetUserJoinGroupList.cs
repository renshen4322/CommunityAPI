using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
   public class GetUserJoinGroupListRequestDto:BaseRequestDto
    {
    }
   public class GetUserJoinGroupListResponseDto : BaseRequestDto
   {
       public GetUserJoinGroupListResponseDto()
       {
           data=new List<GroupDetailInfoDto>();
       }

       public List<GroupDetailInfoDto> data { get; set; }
   }
}
