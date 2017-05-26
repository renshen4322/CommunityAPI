using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Common
{
    public class GetCity : BaseRequestDto
    {
       /// <summary>
       ///省份id 
       /// </summary>
         [Required]    
        public int province_id { get; set; }
    }
    public class GetCityResponse : BaseResponseDto {
        public int id { get; set; }
        public int province_id { get; set; }
        public string name { get; set; }
        public string area_code { get; set; }
    
    }
}
