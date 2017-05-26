using Community.Contact.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Category
{
    /// <summary>
    /// 获取指定对象的目录分类信息
    /// </summary>
    public class GetObjectCategory : BaseRequestDto
    {
        /// <summary>
        /// 查询对象id
        /// </summary>
        [Required]
        public Guid object_id { get; set; }
    }
    public class GetObjectCategoryResponse : BaseResponseDto
    {
        public GetObjectCategoryResponse()
        {
            this.category_list = new List<ObjectCategoryList>();
        }
        public List<ObjectCategoryList> category_list { get; set; }
    }
    public class ObjectCategoryList
    {
        public ObjectCategoryList()
        {
            this.list = new List<ObjectCategory>();
        }
        public string type { get; set; }
        public int type_id { get; set; }
        public List<ObjectCategory> list { get; set; }
    }   
    public class ObjectCategory
    {
        public int id { get; set; }
        public string value { get; set; }
    }

}
