using Community.Contact.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.User
{
    public class UpdateUserImg : CommandRequestDto
    {
        [Required]
        public UserImageTypeEnum type { get; set; }
          [Required]
        public string img_url { get; set; }       
    }
    public class UpdateUserImgResponse : CommandResponseDto { }

}
