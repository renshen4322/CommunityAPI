using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Works
{
    public class GetWorksListByUNameOrWName : QueryRequestDto
    {
        /// <summary>
        /// 作品名或用户昵称(支持模糊搜索)
        /// </summary>
        public string q { get; set; }
    }
    public class GetWorksListByUNameOrWNameResponse : QueryResponseDto
    {
        public GetWorksListByUNameOrWNameResponse()
        {
            data = new List<SearchWorksListdto>();
        }     
        public List<SearchWorksListdto> data { get; set; }

    }
}
