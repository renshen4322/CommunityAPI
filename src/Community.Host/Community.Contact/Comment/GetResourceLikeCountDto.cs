using Community.Contact.Comment.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Comment
{
   public class GetResourceLikeCountRequestDto:BaseRequestDto
    {
        /// <summary>
        /// 资源id
        /// </summary>
        public Guid target_id { get; set; }
        /// <summary>
        /// 目标喜欢类型(Works|Product|News)
        /// </summary>
        public TargetTypeEnum target_type { get; set; }
    }

   public class GetResourceLikeCountResponseDto:BaseResponseDto
   {
       /// <summary>
       /// 喜欢数量
       /// </summary>
       public int count { get; set; }
   }
}
