using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Model
{
    public class SearchWorksModel : IEqualityComparer<SearchWorksModel>
    {
        /// <summary>
        /// 作品id
        /// </summary>
        public Guid WorksId { get; set; }

        public string Style { get; set; }
        /// <summary>
        /// 作品名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 作者名称
        /// </summary>              
        public string Author { get; set; }

        /// <summary>
        /// 作者头像
        /// </summary>
        public string AvatarUrl { get; set; }
        /// <summary>
        /// 作者id
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Introduction { get; set; }
        /// <summary>
        /// 作品缩略图
        /// </summary>
        public string Thumbnail { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime CreatedDate { get; set; }


        public bool Equals(SearchWorksModel x, SearchWorksModel y)
        {
            return x.WorksId.Equals(y.WorksId);
        }

        public int GetHashCode(SearchWorksModel obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
