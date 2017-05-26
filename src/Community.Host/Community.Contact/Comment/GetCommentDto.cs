using System;
using System.Collections.Generic;
using Community.Contact.Comment.Enum;
using System.ComponentModel.DataAnnotations;
using Community.Contact.Enum;

namespace Community.Contact.Comment
{
    public class GetCommentRequestDto : QueryRequestDto
    {
         
        /// <summary>
        /// 评论类型：Works|Product
        /// </summary>
        [Required]
        public TargetTypeEnum target_type { get; set; }

        /// <summary>
        /// 目标资源id
        /// </summary>
         [Required]
        public Guid target_id { get; set; }
        /// <summary>
        /// 筛选条件
        /// </summary>
         [Required]
        public QuerySortTypeEnum sort_type { get; set; }

    }

    public class GetCommentResponseDto : QueryResponseDto
    {
        public GetCommentResponseDto()
        {
            data = new List<CommentContent>();
        }

        public List<CommentContent> data { get; set; }
       


    }

    public class CommentContent
    {

        
        /// <summary>
        /// 评论作者
        /// </summary>
        public Author author { get; set; }
        /// <summary>
        /// 评论id
        /// </summary>
        public Guid id { get; set; }

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
        public Guid? in_reply_to_comment_id { get; set; }
        /// <summary>
        /// 父评论内容
        /// </summary>
        public string in_reply_to_content { get; set; }
        /// <summary>
        /// 父评论人信息
        /// </summary>
        public Author in_reply_to_user { get; set; }

        /// <summary>
        /// 评论时间
        /// </summary>
        public long created_at { get; set; }
    }

    public class Author
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