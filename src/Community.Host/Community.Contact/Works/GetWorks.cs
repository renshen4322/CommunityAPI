using Community.Contact.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Works
{
    /// <summary>
    /// 获取单个作品
    /// </summary>
   public class GetWorks:BaseRequestDto
    {
        /// <summary>
        /// 作品id
        /// </summary>
       public Guid works_id { get; set; }
       
    }
   public class GetWorksResponse : BaseResponseDto
   {
       public GetWorksResponse() {
           products = new List<WorksItems>();
       }
        /// <summary>
        /// 作品id
        /// </summary>
        public Guid id { get; set; }
        /// <summary>
        /// 所有者id
        /// </summary>
        public Guid owner_id { get; set; }
        /// <summary>
        /// 作品名
        /// </summary>      
        public string name { get; set; }

       /// <summary>
       /// 设计时间
       /// </summary>      
       public long design_date { get; set; }
       /// <summary>
       /// 造价
       /// </summary>
       public float? cost { get; set; }
      
       /// <summary>
       /// 实际面积
       /// </summary>
       public float actual_area { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string thumbnail { get; set; }
        /// <summary>
        /// 作品简介
        /// </summary>     
        public string introduction { get; set; }

        /// <summary>
        /// 全景图地址集合，使用逗号分隔多个全景图
        /// </summary>
        public string pano_url { get; set; }
        /// <summary>
        /// 全景图对应缩略图，使用逗号分隔
        /// </summary>
        public string pano_thumbnail { get; set; }

        /// <summary>
        /// 图片集合，使用逗号分隔多个全景图
        /// </summary>
        public string images { get; set; }

       /// <summary>
       /// 产品清单
       /// </summary>
        public List<WorksItems> products { get; set; }

       /// <summary>
       /// 详细描述
       /// </summary>    
       public string description { get; set; }
        /// <summary>
        /// 参与者
        /// </summary>
        public string helper { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public long created_at { get; set; }
   }

    public class WorksItems
    {

        /// <summary>
        /// vidaDesigner端中的拥有者
        /// </summary>
        public int owner_id { get; set; }
        /// <summary>
        /// 产品id
        /// </summary>
        public Guid? product_id { get; set; }
        /// <summary>
        /// 产品源id
        /// </summary>
        public int product_origin_id { get; set; }
        /// <summary>
        /// 产品图片
        /// </summary>
        public string img_url { get; set; }
        /// <summary>
        /// 产品名
        /// </summary>
        public string name { get; set; }
    }
}
