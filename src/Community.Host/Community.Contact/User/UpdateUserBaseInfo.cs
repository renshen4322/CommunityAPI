using Community.Contact.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.User
{
    /// <summary>
    /// 更新用户基础信息
    /// </summary>
    public class UpdateUserBaseInfo : CommandRequestDto
    {
        /// <summary>
        /// 用户昵称
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string nick_name { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [MaxLength(20)]
        public string real_name { get; set; }
        /// <summary>
        /// 出生年份
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// 个人描述
        /// </summary>
        public string introduction { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public GenderEnum gender { get; set; }
       
    }
    public class UpdateUserBaseInfoResponse : CommandResponseDto
    {

    }
}
