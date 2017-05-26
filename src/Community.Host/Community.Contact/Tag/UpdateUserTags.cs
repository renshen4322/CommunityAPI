using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Tag
{
   public class UpdateUserTags:CommandRequestDto
    {
        /// <summary>
        /// 设计师擅长标签(使用空格分隔多个)
        /// </summary>
        [Required]
        public string tags { get; set; }
    }
   public class UpdateUserTagsResponse : CommandResponseDto { 
   }

}
