using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
    /// <summary>
    /// 获取板块下面的组信息
    /// </summary>
   public class GetGroupInfoListRequestDto:BaseRequestDto
    {
       /// <summary>
       /// 板块id
       /// </summary>
        [Required]
       public int classify_id { get; set; }
           
    }

    public class GetGroupInfoListResponseDto : BaseResponseDto
    {

        public GetGroupInfoListResponseDto()
        {
            data = new List<GroupDetailInfoDto>();
        }
        public List<GroupDetailInfoDto> data { get; set; }

    }

  
}
