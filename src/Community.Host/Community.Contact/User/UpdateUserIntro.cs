using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.User
{
   public class UpdateUserIntro:CommandRequestDto
    {
        [Required]
       [MaxLength(100)]
       public string intro { get; set; }
    }
   public class UpdateUserIntroResponse : CommandResponseDto { 
   }
}
