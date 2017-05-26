using Community.Contact.Enum;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.User
{   
    public class GetSingleUser:BaseRequestDto
    {
       /// <summary>
       /// 用户id
       /// </summary>
       public Guid user_id { get; set; }
    }
   public class GetSingleUserResponse : BaseResponseDto {
   
        /// <summary>
        /// 用户id
        /// </summary>
        public Guid id { get; set; }
       /// <summary>
       ///昵称
       /// </summary>
       public string nick_name { get; set; }
       
       /// <summary>
       /// 真实姓名
       /// </summary>
       public string real_name { get; set; }
       /// <summary>
       /// 性别
       /// </summary>
       public string gender { get; set; }
       /// <summary>
       /// 头像
       /// </summary>
       public string avatar_url { get; set; }
       /// <summary>
       /// 封面
       /// </summary>
        public string cover_url { get; set; }

       /// <summary>
       /// 出生年份
       /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// 背景
        /// </summary>
        public string Background_url { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string intro { get; set; }

        public string country_name { get; set; }
       /// <summary>
       /// 省份
       /// </summary>
       public string province_name { get; set; }
       /// <summary>
       /// 城市
       /// </summary> 
       public string city_name { get; set; }
       /// <summary>
       /// 区县
       /// </summary> 
       public string district_name { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public string user_role { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public long created_at { get; set; }
      
      
   }
  
}
