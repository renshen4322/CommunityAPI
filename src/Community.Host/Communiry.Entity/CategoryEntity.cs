using Community.Core.Data;
using System;

namespace Communiry.Entity
{
   public class CategoryEntity:BaseEntity
    {
       public int Id { get; set; }
       public int TypeId { get; set; }
       public int ParentId { get; set; }
       public string Name { get; set; }
        public string EnName { get; set; }
        public string SysName { get; set; }
        public bool OffLine { get; set; }
       public bool IsHot { get; set; }
        public bool IsMultiple { get; set; }
        public bool IsSystemCategory { get; set; }
        public int DisplayIndex { get; set; }
       public DateTime CreatedDate { get; set; }
       
    }
}
