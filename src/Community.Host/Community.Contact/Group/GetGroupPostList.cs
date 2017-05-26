using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Contact.Group.Enum;

namespace Community.Contact.Group
{
    public class GetGroupPostListRequestDto : QueryRequestDto
    {
        /// <summary>
        /// 组id
        /// </summary>
        [Required]
        public int group_id { get; set; }

        /// <summary>
        /// 排序类型
        /// </summary>
       // [Required]
      //  public PostSortTypeEnum sort_type { get; set; }
    }
    public class GetGroupPostListResponseDto : QueryResponseDto
    {
        public GetGroupPostListResponseDto()
        {
            data=new List<PostInfoDto>();
        }
        public List<PostInfoDto> data { get; set; }

    }
    
   
}
