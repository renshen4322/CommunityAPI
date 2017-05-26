using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Comment
{
    /// <summary>
    /// 获取用户对某资源评论内容的喜欢列表
    /// </summary>
    public class GetUserLikeListRequestDto : CommandRequestDto
    {
        /// <summary>
        /// 目标对象id(文章|产品id)
        /// </summary>
        public Guid id { get; set; }

    }
    /// <summary>
    /// 用户对资源评论的喜欢列表
    /// </summary>
    public class GetUserLikeListResponseDto : CommandResponseDto
    {
        public GetUserLikeListResponseDto()
        {
            likes_list = new List<Guid>();
        }
        /// <summary>
        /// 目标对象id(文章|产品id)
        /// </summary>
        public Guid target_id { get; set; }
        /// <summary>
        /// 喜欢列表
        /// </summary>
        public List<Guid> likes_list { get; set; }

    }

}
