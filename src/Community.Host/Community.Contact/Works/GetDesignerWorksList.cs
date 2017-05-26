using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Works
{

    /// <summary>
    /// 获取某个设计师的作品列表
    /// </summary>
    public class GetDesignerWorksList : QueryRequestDto
    {
        public Guid designer_id { get; set; }
    }
    public class GetDesignerWorksListResponse : QueryResponseDto
    {
        public GetDesignerWorksListResponse() {

            this.Data = new List<DesignerWorksInfo>();
        }
        public List<DesignerWorksInfo> Data;
       
    }
    public class DesignerWorksInfo {
        /// <summary>
        /// 作品id
        /// </summary>
        public Guid works_id { get; set; }

        public string style { get; set; }
        /// <summary>
        /// 作品名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 所有者id
        /// </summary>
        public Guid owner_id { get; set; }
        /// <summary>
        /// 作品缩略图
        /// </summary>
        public string thumbnail { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string introduction { get; set; }
        /// <summary>
        /// 全景图地址集合,使用逗号分割多张全景图
        /// </summary>
        public string pano_url { get; set; }
        /// <summary>
        /// 全景图缩略图集合，使用逗号分割多张缩略图
        /// </summary>
        public string pano_thumbnail { get; set; }
        /// <summary>
        /// 图片集合，使用逗号分割多张图片
        /// </summary>
        public string images { get; set; }
        /// <summary>
        /// 图片缩略图，使用逗号分割多张图片缩略图
        /// </summary>
        public string image_thumbnail { get; set; }
        /// <summary>
        /// 设计时间
        /// </summary>
        public long design_date { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public long created_at { get; set; }
    }
}
