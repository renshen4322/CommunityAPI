using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Tag
{
  public  class GetUserTags:BaseRequestDto
    {
      /// <summary>
      /// 用户id
      /// </summary>
      [Required]
      public Guid id { get; set; }
    }

  public class GetUserTagsResponse : BaseResponseDto
  {
        public GetUserTagsResponse() {
            this.tags = new List<string>();
        }
      public List<string> tags { get; set; }
  }

}
