using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
    public class GetHotPostRequestDto : QueryRequestDto
    {
    }
    public class GetHotPostResponseDto : QueryRequestDto
    {
        public GetHotPostResponseDto()
        {
            data = new List<PostInfoDto>();
        }

        public List<PostInfoDto> data { get; set; }
    }
}
