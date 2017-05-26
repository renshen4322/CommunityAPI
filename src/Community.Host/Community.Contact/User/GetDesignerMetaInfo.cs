using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.User
{
  public class GetDesignerMetaInfo:CommandRequestDto
    {
      /// <summary>
      /// 用户id
      /// </summary>
      [Required]
      public Guid user_id { get; set; }
    }
  public class GetDesignerMetaInfoResponse : CommandResponseDto
  {
      /// <summary>
      /// 设计年限
      /// </summary>
      public int design_age { get; set; }
  }
}
