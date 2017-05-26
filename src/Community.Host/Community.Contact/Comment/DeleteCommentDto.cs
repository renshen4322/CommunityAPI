using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Comment
{
   public class DeleteCommentRequestDto:CommandRequestDto
    {
       /// <summary>
       /// 评论id
       /// </summary>
       [Required]
       public Guid comment_id { get; set; }
    }
   public class DeleteCommentResponseDto : CommandResponseDto
   {
   }
}
