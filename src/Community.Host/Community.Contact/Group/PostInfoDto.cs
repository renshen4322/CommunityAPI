using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
   public class PostInfoDto
    {
        public PostInfoDto()
        {
            author = new GroupAuthor();
        }

        /// <summary>
        /// 帖子id
        /// </summary>
        public int post_id { get; set; }
        /// <summary>
        /// 小组id
        /// </summary>
        public int group_id { get; set; }
        /// <summary>
        /// 小组名
        /// </summary>
        public string group_name { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public int comment_count { get; set; }

        /// <summary>
        /// 发帖时间
        /// </summary>
        public long created_at { get; set; }

        /// <summary>
        /// 发帖人
        /// </summary>
        public GroupAuthor author { get; set; }
    }
}
