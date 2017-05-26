using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.User
{
   public class UpdateUserNickName:CommandRequestDto
    {
         [Required]
       public string nick_name { get; set; }

    }
   public class UpdateUserNickNameResponse : CommandResponseDto { }
}
