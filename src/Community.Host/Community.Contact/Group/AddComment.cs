using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
    /// <summary>
    /// 添加帖子评论dto
    /// </summary>
  public  class AddCommentRequestDto:CommandRequestDto
    {
      /// <summary>
      /// 帖子id
      /// </summary>
      [Required]
      public int post_id { get; set; }
      /// <summary>
      /// 追评父级id(如果存在的话)
      /// </summary>
      public int reply_comment_id { get; set; }
      /// <summary>
      /// 评论内容
      /// </summary>
      [Required]
      [MaxLength(5000)]
      
      public string content { get; set; }
    }

  public class AddCommentResponseDto : CommandResponseDto
  {
      /// <summary>
      /// 评论id
      /// </summary>
      public int comment_id { get; set; }

      /// <summary>
      /// 评论总数
      /// </summary>
      public int count { get; set; }

      /// <summary>
      /// 失败原因
      /// </summary>
      public string msg { get; set; }
      /// <summary>
      /// true：成功|false：失败
      /// </summary>
      public bool result { get; set; }
  }
}
