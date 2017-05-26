using Community.Contact.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.User
{
   public class AddUser:CommandRequestDto
    {
   
      
       /// <summary>
       /// user's nickname
       /// </summary>
       [Required]
       [MaxLength(20)]
       public string nick_name { get; set; }
       /// <summary>
       /// user's real name
       /// </summary>
       [MaxLength(20)]
       public string real_name { get;set; }

       /// <summary>
       /// 出生年份
       /// </summary>
       public string birthday { get; set; }
       public GenderEnum gender { get; set; }

       public UserRoleEnum user_role { get; set; }
       /// <summary>
       /// country id
       /// </summary>
       public int? country_id { get; set; }
       /// <summary>
       /// province id
       /// </summary>
       public int? province_id { get; set; }
       /// <summary>
       /// city id
       /// </summary>
       public int? city_id { get; set; }
       /// <summary>
       /// district id
       /// </summary>
       public int? district_id { get; set; }
    }
    public class AddUserResponse:CommandResponseDto{
        public Guid user_id { get; set; }
    }
}
