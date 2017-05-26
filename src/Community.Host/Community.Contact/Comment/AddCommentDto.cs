using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Contact.Comment.Enum;


namespace Community.Contact.Comment
{
    /// <summary>
    /// 添加评论dto
    /// </summary>
   public class AddCommentDto:CommandRequestDto
    {
       /// <summary>
       /// 评论对象id
       /// </summary>
       [Required]
       public Guid target_id { get; set; }
        /// <summary>
        /// 评论类型(Works|Product)
        /// </summary>
        [Required]
        public TargetTypeEnum target_type { get; set; }
     
        /// <summary>
        /// 追评父级id(如果存在的话)
        /// </summary>
        public Guid? reply_comment_id { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        [Required]
        [MaxLength(5000)]
        public string content { get; set; }

    }

   /// <summary>
   /// 添加评论响应dto
   /// </summary>
   public class AddCommentResponse : CommandResponseDto {
        /// <summary>
        /// 新评论id
        /// </summary>
       public Guid comment_id { get; set; }
       /// <summary>
       /// 当前评论总数
       /// </summary>
       public int count { get; set; }

    }
}
