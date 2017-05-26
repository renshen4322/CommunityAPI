using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
 
    /// <summary>
    /// 获取板块下面所有小组的最新帖子列表
    /// </summary>
    public class GetClassifyNewPostListRequestDto:QueryRequestDto
    {
        /// <summary>
        /// 板块id
        /// </summary>
        public int classify_id { get; set; }
    }
    public class GetClassifyNewPostListResponseDto : QueryResponseDto
    {
        public GetClassifyNewPostListResponseDto()
        {
            data=new List<PostInfoDto>();
        }
        public List<PostInfoDto> data { get; set; }

    }
   
}
