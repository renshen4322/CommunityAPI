using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
    /// <summary>
    /// 获取帖子详情
    /// </summary>
   public class GetGroupPostDetailRequestDto:BaseRequestDto
    {
       /// <summary>
       /// 帖子id
       /// </summary>
       [Required]
       public int post_id { get; set; }
    }

    public class GetGroupPostDetailResponseDto : BaseResponseDto
    {
        public GetGroupPostDetailResponseDto()
        {
            author=new GroupAuthor();
            group_info=new GroupInfoDto();
        }

        /// <summary>
        /// 帖子id
        /// </summary>
        public int post_id { get; set; }
        /// <summary>
        /// 帖子标题
        /// </summary>
        public string post_title { get; set; }
        /// <summary>
        /// 帖子内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 喜欢总数
        /// </summary>
        public int like_count { get; set; }
        /// <summary>
        /// 板块id
        /// </summary>
        public int classify_id { get; set; }
        /// <summary>
        /// 评论总数
        /// </summary>
        public int commment_count { get; set; }
        /// <summary>
        /// 收藏数
        /// </summary>
        public int collect_count { get; set; }
        /// <summary>
        /// 发帖人
        /// </summary>
        public GroupAuthor author { get; set; }
        /// <summary>
        /// 圈子信息
        /// </summary>
        public GroupInfoDto group_info { get; set; }
        /// <summary>
        /// 发帖时间
        /// </summary>
        public long created_at { get; set; }
    
    }

    
}
