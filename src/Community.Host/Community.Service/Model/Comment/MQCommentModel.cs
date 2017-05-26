using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Model.Comment
{
    public class MQCommentModel
    {
        /// <summary>
        /// 评论id
        /// </summary>
        public Guid comment_id { get; set; }
        /// <summary>
        /// 评论对象id
        /// </summary>
        public Guid target_id { get; set; }
        /// <summary>
        /// 评论人id
        /// </summary>
        public Guid author_id { get; set; }
        /// <summary>
        /// 评论对象类型
        /// </summary>
        public TargetTypeEnum target_type { get; set; }
        /// <summary>
        /// 父评论id
        /// </summary>
        public Guid? parent_id { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 评论创建时间戳
        /// </summary>
        public long created_at { get; set; }
    }
}
