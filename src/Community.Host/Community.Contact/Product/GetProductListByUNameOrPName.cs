using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Product
{
  public  class GetProductListByUNameOrPName:QueryRequestDto
    {
      /// <summary>
        /// 产品名或用户昵称(支持模糊搜索)
      /// </summary>
      public string q { get; set; }
    }
  public class GetProductListByUNameOrPNameResponse : QueryResponseDto
  {
      public GetProductListByUNameOrPNameResponse()
        {
            data = new List<ProductIntro>();
        }
        public List<ProductIntro> data { get; set; }
  }
}
