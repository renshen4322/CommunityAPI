using Community.Contact.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Product
{
    public class GetProductDetail:BaseRequestDto
    {
        /// <summary>
        /// 产品id
        /// </summary>
        public Guid product_id { get; set; }
    }
    public class GetProductDetailResponse : BaseResponseDto
    {
        /// <summary>
        /// 产品id
        /// </summary>
        public Guid id { get; set; }
        /// <summary>
        /// 所有者id
        /// </summary>
        public Guid owner_id { get; set; }
        /// <summary>
        /// 物品名
        /// </summary>     
        public string name { get; set; }
        /// <summary>
        /// 物品上传方式
        /// </summary>     
        public ProductCreateTypeEnum upload_type { get; set; }
        /// <summary>
        /// 源物品ID，若选择导入方式，此字段必传
        /// </summary>
        public int origin_id { get; set; }
      
        /// <summary>
        /// 价格
        /// </summary>
        public float? cost { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string thumbnail { get; set; }
        /// <summary>
        /// 简介
        /// </summary>     
        public string introduction { get; set; }

      
        /// <summary>
        /// 图片集合，使用逗号分隔多张
        /// </summary>
        public string images { get; set; }
        /// <summary>
        /// 图片集合，使用逗号分隔多张
        /// </summary>
        public string images_thumbnail { get; set; }
        /// <summary>
        /// 详细描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public long created_at { get; set; }
    }
}
