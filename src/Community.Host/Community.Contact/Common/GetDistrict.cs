using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Common
{
   public class GetDistrict:BaseRequestDto
    {
       /// <summary>
       /// 城市id
       /// </summary>
       [Required]  
       public int city_id { get; set; }
    }
   public class GetDistrictResponse : BaseResponseDto {
       public int id { get; set; }
       public int city_id { get; set; }
       public string name { get; set; }
       public string post_code { get; set; }
   
   }
}
