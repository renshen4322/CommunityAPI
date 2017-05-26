using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Works
{
    public class SearchWorksList : QueryRequestDto
    {
        /// <summary>
        /// 搜索分类集合,使用','号分割每个标识符
        /// </summary>
       public string type_ids { get; set; }
    }
    public class SearchWorksListResponse : QueryResponseDto
    {
        public SearchWorksListResponse() {
            data = new List<SearchWorksListdto>();
        }     
        public List<SearchWorksListdto> data { get; set; }


    }
    public class SearchWorksListdto {
      
        /// <summary>
        /// 作品id
        /// </summary>
        public Guid works_id { get; set; }
        /// <summary>
        /// 风格
        /// </summary>
        public string style { get; set; }
        /// <summary>
        /// 作品名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 作者名称
        /// </summary>              
        public string author { get; set; }

        /// <summary>
        /// 作者头像
        /// </summary>
        public string avatar_url { get; set; }
        /// <summary>
        /// 作者id
        /// </summary>
        public Guid author_id { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string introduction { get; set; }
        /// <summary>
        /// 作品缩略图
        /// </summary>
        public string thumbnail { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public long created_at { get; set; }
    }
}
