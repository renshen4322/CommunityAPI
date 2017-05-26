using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.User
{
   public class GetTokenByCode:BaseRequestDto
    {
       /// <summary>
       /// 获取token的临时code
       /// </summary>
       [Required]
       public string code { get; set; }
    }
   public class GetTokenByCodeResponse : BaseResponseDto
   {
       /// <summary>
       /// token格式：{"access_token":"xxx","Scheme": "bearer"}
       /// </summary>
       public object token { get; set; }
   }
}
