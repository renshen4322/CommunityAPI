using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
    /// <summary>
    /// 获取帖子评论列表
    /// </summary>
   public class GetPostCommentListRequestDto:QueryRequestDto
    {
       /// <summary>
       ///帖子id
       /// </summary>
       [Required]
       public int post_id { get; set; }
    }
   public class GetPostCommentListResponseDto : QueryResponseDto
   {
       public GetPostCommentListResponseDto()
       {
           data = new List<PostComment>();
       }

       public List<PostComment> data { get; set; }



   }

   public class PostComment
   {


       /// <summary>
       /// 评论人
       /// </summary>
       public CommentAuthor author { get; set; }
       /// <summary>
       /// 评论id
       /// </summary>
       public int id { get; set; }

       /// <summary>
       ///  评论内容
       /// </summary>
       public string content { get; set; }

       /// <summary>
       /// 喜欢数
       /// </summary>
       public int likes_count { get; set; }
       /// <summary>
       /// 父评论id
       /// </summary>
       public int in_reply_to_comment_id { get; set; }
       /// <summary>
       /// 父评论内容
       /// </summary>
       public string in_reply_to_content { get; set; }
       /// <summary>
       /// 父评论人信息
       /// </summary>
       public CommentAuthor in_reply_to_user { get; set; }

       /// <summary>
       /// 评论时间
       /// </summary>
       public long created_at { get; set; }
   }

   public class CommentAuthor
   {
       /// <summary>
       /// 用户id
       /// </summary>
       public Guid id { get; set; }
       /// <summary>
       /// 用户角色
       /// </summary>
       public string role { get; set; }
       /// <summary>
       /// 个人简介
       /// </summary>
       public string bio { get; set; }
       /// <summary>
       /// 名字
       /// </summary>
       public string name { get; set; }
       /// <summary>
       /// 头像
       /// </summary>
       public string avatar_url { get; set; }
       /// <summary>
       /// 是否是作者
       /// </summary>
       public bool is_org { get; set; }

   }
}
