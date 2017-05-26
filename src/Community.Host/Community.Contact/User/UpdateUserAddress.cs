using Community.Contact.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.User
{
  public  class UpdateUserAddress:CommandRequestDto
    {

      public AddressTypeEnum address_type { get; set; }
        /// <summary>
        /// 国家id
        /// </summary>
        public int country_id { get; set; }
        /// <summary>
        /// 省id
        /// </summary>
        [Required]
        public int province_id { get; set; }
        /// <summary>
        /// 市id
        /// </summary>
         [Required]
        public int city_id { get; set; }
        /// <summary>
        /// 区县id
        /// </summary>
        [Required]
        public int district_id { get; set; }
        /// <summary>
        /// 详细街道地址
        /// </summary>
        [MaxLength(500)]
        public string street { get; set; }
    }

  public class UpdateUserAddressResponse : CommandResponseDto { }
}
