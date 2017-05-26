using Community.Contact.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Category
{
   public class UpdateObjectCategory:CommandRequestDto
    {
       /// <summary>
       /// 对象id
       /// </summary> 
       [Required]
       public Guid object_id { get; set; }
       /// <summary>
       /// 分类id集合(多个id之间用逗号分割例如'1,2,3,4')
       /// </summary>
        [Required]
       public string categorie_ids { get; set; }

        /// <summary>
        /// 需要修改的资源类型
        /// </summary>
        [Required]
        public UpdateCategoryTypeEnum type { get; set; }
    }
   public class UpdateObjectCategoryResponse : CommandResponseDto
   { }
   
}
