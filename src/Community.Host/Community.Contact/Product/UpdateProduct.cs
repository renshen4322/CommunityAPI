using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Product
{
   public class UpdateProduct:CommandRequestDto
    {
        /// <summary>
        /// 产品id
        /// </summary>
       public Guid product_id { get; set; }
        /// <summary>
        /// 物品名
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string name { get; set; }
       
        /// <summary>
        /// 价格
        /// </summary>
        public float? cost { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        [Required]
        public string thumbnail { get; set; }
        
        /// <summary>
        /// 简介
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string introduction { get; set; }

        /// <summary>
        /// 图片集合，使用逗号分隔多张
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string images { get; set; }
        /// <summary>
        /// 图片集合，使用逗号分隔多张
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string images_thumbnail { get; set; }
        /// <summary>
        /// 详细描述
        /// </summary>
        [Required]
        public string description { get; set; }
    }
   public class UpdateProductResponse : CommandResponseDto
   { }
}
