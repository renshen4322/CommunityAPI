using Community.Contact.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Category
{
   public class GetTargetAllCategory:BaseRequestDto
    {
       [Required]
       public CategoryTypeEnum type { get; set; }
    }
   public class GetTargetAllCategoryResponse : BaseResponseDto
   {
       public GetTargetAllCategoryResponse() {
           this.category_list = new List<CategoryList>();
       }
        public List<CategoryList> category_list{get;set;}
   
   }
   public class CategoryList
   {
       public CategoryList()
       {
           this.list = new List<Category>();
       }
       public string type { get; set; }
       public int type_id { get; set; }
       public bool is_multiple { get; set; }
       public List<Category> list { get; set; }
   }
   public class Category
   {
       public int id { get; set; }
       public string value { get; set; }
       public bool is_multiple { get; set; }
   }
   
}
