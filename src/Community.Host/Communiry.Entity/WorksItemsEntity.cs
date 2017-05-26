using Community.Core.Data;
using System;

namespace Communiry.Entity
{
    public class WorksItemsEntity : BaseEntity
    {
      public int Id { get; set; }
        /// <summary>
        /// 作品id
        /// </summary>
      public Guid WorksId { get; set; }
        /// <summary>
        /// 源所有人id
        /// </summary>
      public int OwierOriginId { get; set; }
        /// <summary>
        /// 产品源id
        /// </summary>
      public int ProductOriginId { get; set; }
        /// <summary>
        /// 社区中产品id
        /// </summary>
      public Guid? ProductId { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
      public string ImgUrl { get; set; }
      /// <summary>
      /// 物品名
      /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
