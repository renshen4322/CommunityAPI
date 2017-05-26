using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.User
{
    public class GetUserInfo : CommandRequestDto
    {

    }
    public class GetUserInfoResponse : CommandResponseDto 
    {
        public Guid id { get; set; }
        /// <summary>
        ///昵称
        /// </summary>
        public string nick_name { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string avatar_url { get; set; }
        
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string real_name { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string intro { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        /// 出生年份
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public int province_id { get; set; }
        /// <summary>
        /// 城市
        /// </summary> 
        public int city_id { get; set; }
        /// <summary>
        /// 区县
        /// </summary> 
        public int district_id { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 角色类型
        /// </summary>
        public string user_role { get; set; }
        /// <summary>
        /// 加入时间
        /// </summary>
        public long created_at { get; set; }
    }
}
