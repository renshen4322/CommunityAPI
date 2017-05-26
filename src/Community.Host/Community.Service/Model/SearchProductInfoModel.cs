using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Model
{
    public class SearchProductInfoModel : IEqualityComparer<SearchProductInfoModel>
    {
        /// <summary>
        /// 产品id
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// 风格
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 造价
        /// </summary>
        public float? Cost { get; set; }
        /// <summary>
        /// 产品名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 所有者昵称
        /// </summary>              
        public string Author { get; set; }
        /// <summary>
        /// 所有者头像
        /// </summary>
        public string AvatarUrl { get; set; }
        /// <summary>
        /// 所有者id
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Introduction { get; set; }
        /// <summary>
        /// 产品缩略图
        /// </summary>
        public string Thumbnail { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        public bool Equals(SearchProductInfoModel x, SearchProductInfoModel y)
        {
            return x.ProductId.Equals(y.ProductId);
        }

        public int GetHashCode(SearchProductInfoModel obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
