using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
    public class GroupDetailInfoDto
    {
        /// <summary>
        /// 组id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 组名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 组发起人
        /// </summary>
        //public string originator { get; set; }

        /// <summary>
        /// 所属板块id
        /// </summary>
        public int classify_id { get; set; }
        /// <summary>
        /// 板块名称
        /// </summary>
        public string classify_name { get; set; }

        ///<summary>
        ///组描述
        /// </summary>
        public string descripation { get; set; }

        /// <summary>
        /// 组头像
        /// </summary>
        public string cover_url { get; set; }

        /// <summary>
        /// 是否热门
        /// </summary>
        public bool is_hot { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// 组创建时间
        /// </summary>
        public long created_at { get; set; }
    }

}
