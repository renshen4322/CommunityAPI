using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Product
{
    /// <summary>
    /// 获取导入的产品id数组
    /// </summary>
    public class GetImportPorductIdList:BaseRequestDto
    {
    }

    public class GetImportPorductIdListResponse : BaseResponseDto {
        public GetImportPorductIdListResponse()
        {
            product_ids = new List<IpmortProductData>();

        }
        /// <summary>
        /// 用户id
        /// </summary>
        public Guid user_id { get; set; }
        /// <summary>
        /// 已导入的源id列表
        /// </summary>
        public List<IpmortProductData> product_ids { get; set; }
    }
    public class IpmortProductData
    {
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 导入时间
        /// </summary>
        public long created_at { get; set; }
    }
}
