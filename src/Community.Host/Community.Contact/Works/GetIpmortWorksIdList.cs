using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Works
{
    /// <summary>
    /// 获取导入的作品id数组
    /// </summary>
    public class GetIpmortWorksIdList : BaseRequestDto
    {
    }

    public class GetIpmortWorksIdListResponse : BaseResponseDto {
        public GetIpmortWorksIdListResponse() {
            works_ids = new List<IpmortWorksData>();

        }
        /// <summary>
        /// 用户id
        /// </summary>
        public Guid user_id { get; set; }
        /// <summary>
        /// 已导入的id列表
        /// </summary>
        public List<IpmortWorksData> works_ids { get; set; }
    }
    public class IpmortWorksData{
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
