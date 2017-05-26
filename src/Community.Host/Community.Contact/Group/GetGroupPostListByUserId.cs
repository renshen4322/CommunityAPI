using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
   public class GetGroupPostListByUserIdRequestDto:QueryRequestDto
    {
    }

    public class GetGroupPostListByUserIdResponseDto : QueryResponseDto
    {
        public GetGroupPostListByUserIdResponseDto()
        {
            data = new List<PostInfoDto>();
        }

        public List<PostInfoDto> data { get; set; }
    }
}
