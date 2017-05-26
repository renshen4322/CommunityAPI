using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Comment
{
   /// <summary>
   /// 举报接口
   /// </summary>
   public class ReportCommentRequestDto:CommandRequestDto
    {
       /// <summary>
       ///  评论id
       /// </summary>
       [Required]
       public Guid comment_id { get; set; }

       /// <summary>
       /// 原因
       /// </summary>
       [Required]
       public string report_reason { get; set; }
   }

    public class ReportCommentResponseDto : CommandResponseDto
    {
        /// <summary>
        /// -1:失败|0：成功
        /// </summary>
        public int success { get; set; }
        /// <summary>
        /// 失败原因
        /// </summary>
        public string msg { get; set; }
    }
}
