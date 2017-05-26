using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
    public class GetGroupMemberRequestDto:BaseRequestDto
    {
        /// <summary>
        /// 组id
        /// </summary>
        [Required]
        public int group_id { get; set; }
    }

    public class GetGroupMemberResponseDto : BaseResponseDto
    {
        public GetGroupMemberResponseDto()
        {
            data = new List<MemberInfoDto>();
        }

        public List<MemberInfoDto> data { get; set; }
    }
  
}
