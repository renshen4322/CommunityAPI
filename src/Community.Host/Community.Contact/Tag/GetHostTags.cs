using Community.Contact.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Tag
{
   public class GetUserHostTags:BaseRequestDto
    {
       /// <summary>
       /// 用户角色
       /// </summary>
       [Required]
       public UserRoleEnum type { get; set; }
    }
   public class GetUserHostTagsResponse : BaseResponseDto {
        public GetUserHostTagsResponse() {
            this.hot_tags = new List<string>();
        }
       public List<string> hot_tags { get; set; }
   }

}
