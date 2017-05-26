using Community.Contact.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Works
{
   public class UpdateWorks : CommandRequestDto
    {
       public UpdateWorks() {
           ItemList = new List<ItemInfo>();
       }
       /// <summary>
       /// 作品id
       /// </summary>
       [Required]
       public Guid works_id { get; set; }
        /// <summary>
        /// 作品名
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string name { get; set; }

        /// <summary>
        /// 设计时间
        /// </summary>
        [Required]
        public DateTime design_date { get; set; }
        /// <summary>
        /// 造价
        /// </summary>
        public float? cost { get; set; }

       
        /// <summary>
        /// 实际面积
        /// </summary>
        public int actual_area { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string thumbnail { get; set; }
        /// <summary>
        /// 作品简介
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string introduction { get; set; }

        /// <summary>
        /// 全景图地址集合，使用逗号分隔多张
        /// </summary>
        [MaxLength(1000)]
        public string pano_url { get; set; }
        /// <summary>
        /// 全景图缩略图集合，使用逗号分隔多张
        /// </summary>
        [MaxLength(1000)]
        public string pano_thumbnail { get; set; }
        /// <summary>
        /// 图片集合，使用逗号分隔多张
        /// </summary>
       // [Required]
        [MaxLength(1000)]
        public string images { get; set; }
        /// <summary>
        /// 图片集合，使用逗号分隔多张
        /// </summary>
      //  [Required]
        [MaxLength(1000)]
        public string images_thumbnail { get; set; }
        /// <summary>
        /// 物品清单
        /// </summary>      
        public List<ItemInfo> ItemList { get; set; }

        /// <summary>
        /// 详细描述
        /// </summary>
        [Required]
        public string description { get; set; }
    }
   public class UpdateWorksRespons:CommandResponseDto { }

}
