using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
    /// <summary>
    /// 获取板块信息
    /// </summary>
    public class GetClassifyListRequestDto : BaseRequestDto
    {

    }

    public class GetClassifyListResponseDto : BaseResponseDto
    {
        public GetClassifyListResponseDto()
        {
            data = new List<ClassifyInfoDto>();
        }

        public List<ClassifyInfoDto> data { get; set; }
    }
}
;