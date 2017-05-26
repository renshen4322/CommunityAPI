using Community.Contact.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Product
{
    /// <summary>
    /// 根据供应商id，获取起上传的产品列表
    /// </summary>
    public class GetProductListByUserId:QueryRequestDto
    {
        /// <summary>
        /// 供应商id
        /// </summary>
        [Required]
        public Guid user_id { get; set; }
    }
   public class GetProductListByUserIdResponse : QueryResponseDto
    {
        public GetProductListByUserIdResponse()
        {

            this.Data = new List<ProductInfo>();
        }
        public List<ProductInfo> Data;

    }
    public class ProductInfo
    {
        /// <summary>
        /// 产品id
        /// </summary>
        public Guid product_id { get; set; }
        /// <summary>
        /// 产品名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 所有者id
        /// </summary>
        public Guid owner_id { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string introduction { get; set; }
        /// <summary>
        /// 图片集合，使用逗号分割多张图片
        /// </summary>
        public string images { get; set; }
        /// <summary>
        /// 图片缩略图，使用逗号分割多张图片缩略图
        /// </summary>
        public string image_thumbnail { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public long created_at { get; set; }
        /// <summary>
        /// 风格，用逗号分割
        /// </summary>
        public string style { get; set; }
    }
}
