using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Contact.Comment.Enum;

namespace Community.Contact.Comment
{
    public class LikeRequestDto : CommandRequestDto
    {
        /// <summary>
        /// 目标喜欢类型(Works|Product|News)
        /// </summary>
        public TargetTypeEnum target_type { get; set; }

        /// <summary>
        /// 资源id
        /// </summary>
        public Guid target_id { get; set; }
        /// <summary>
        /// 喜欢对象id（如果喜欢类型是评论的话 必填）
        /// </summary>
        public Guid? comment_id { get; set; }


    }

    public class LikeResponseDto : CommandResponseDto
    {
        /// <summary>
        /// 当前喜欢状态
        /// </summary>
        public bool is_liked { get; set; }
    }
}
