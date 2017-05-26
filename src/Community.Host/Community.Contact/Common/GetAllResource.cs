using Community.Contact.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Common
{
    /// <summary>
    /// 集合资源搜索
    /// </summary>
   public class GetAllResource:QueryRequestDto
    {
       /// <summary>
       /// 资源类型
       /// </summary>
       [Required]
       public ResourceTypeEnum cat { get; set; }
       /// <summary>
       /// 查询关键字
       /// </summary>
       [Required]
       public string q { get; set; }
    }

   public class GetAllResourceResponse : QueryResponseDto {
       public List<ResourceData> Data { get; set; }
   }
   public class ResourceData {
       /// <summary>
       /// 资源类型
       /// [ 'Works', 'Product', 'News', 'Customer', 'Supplier', 'Designer']
       /// </summary>
       public string resource_type { get; set; }

       public Guid resource_id { get; set; }
       /// <summary>
       /// 标题
       /// </summary>
       public string title { get; set; }
       /// <summary>
       /// 缩略图
       /// </summary>
       public string thumbnail { get; set; }
       /// <summary>
       /// 简介
       /// </summary>
       public string intro { get; set; }
       /// <summary>
       /// 链接
       /// </summary>
       public string resource_url { get; set; }
   }
}
