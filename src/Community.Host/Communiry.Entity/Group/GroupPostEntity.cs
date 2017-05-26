using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Core.Data;

namespace Communiry.Entity.Group
{
    public class GroupPostEntity:BaseEntity
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Title { get; set; }
        
        public Guid AuthorId { get; set; }
        public bool IsOffLine { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public int CollectCount { get; set; }
        public DateTime GMTCreate { get; set; }
        public DateTime? GMTModified { get; set; }

    }
}
