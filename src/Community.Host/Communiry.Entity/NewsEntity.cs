using Community.Core.Data;
using System;

namespace Communiry.Entity
{
    public class NewsEntity : BaseEntity
    {
       public Guid Id { get; set; } 
       public string Title { get; set; }
       public string NewsUrl { get; set; }
       public string Thumbnail { get; set; }
       public string Introduction { get; set; }
       public bool OffLine { get; set; }
       public bool IsHot { get; set; }
       public DateTime CreatedDate { get; set; }
    }
}
