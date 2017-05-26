using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
   public class MemberInfoDto
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public Guid id { get; set; }
        /// <summary>
        /// 用户角色
        /// </summary>
        public string role { get; set; }
        /// <summary>
        /// 个人简介
        /// </summary>
        public string bio { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string avatar_url { get; set; }

       /// <summary>
       /// 加入时间
       /// </summary>
        public long created_at { get; set; }
    }
}
