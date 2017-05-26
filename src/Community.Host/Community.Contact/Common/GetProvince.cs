using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Common
{
    public class GetProvince:BaseRequestDto
    {

    }
    public class GetProvinceResponse : BaseResponseDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public int orderId { get; set; }
    }
}
