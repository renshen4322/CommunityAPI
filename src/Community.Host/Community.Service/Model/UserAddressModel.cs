using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Model
{
   public class UserAddressModel
    {
       public string ProvinceName { get; set; }
       public int ProvinceId { get; set; }
       public string CityName { get; set; }
       public int CityId { get; set; }
       public string DistrictName { get; set; }
       public int DistrictId { get; set; }
    }
}
