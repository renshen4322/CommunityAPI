using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Product
{
    /// <summary>
    /// 搜索分类集合,使用','号分割每个标识符
    /// </summary>
    public class SearchProductByType : QueryRequestDto
    {
        /// <summary>
        /// 搜索分类集合,使用','号分割每个标识符
        /// </summary>
        public string type_ids { get; set; }
    }

    public class SearchProductByTypeResponse : QueryResponseDto {

        public SearchProductByTypeResponse()
        {
            data = new List<ProductIntro>();
        }
        public List<ProductIntro> data { get; set; }


    }
    public class ProductIntro
    {

        /// <summary>
        /// 产品id
        /// </summary>
        public Guid product_id { get; set; }
        /// <summary>
        /// 风格
        /// </summary>
        public string style { get; set; }
        /// <summary>
        /// 造价
        /// </summary>
        public float? cost { get; set; }
        /// <summary>
        /// 产品名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 所有者昵称
        /// </summary>              
        public string author { get; set; }
        /// <summary>
        /// 所有者头像
        /// </summary>
        public string avatar_url { get; set; }
        /// <summary>
        /// 所有者id
        /// </summary>
        public Guid author_id { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string introduction { get; set; }
        /// <summary>
        /// 产品缩略图
        /// </summary>
        public string thumbnail { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public long created_at { get; set; }
    }
}
