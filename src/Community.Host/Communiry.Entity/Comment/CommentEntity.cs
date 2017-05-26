using Community.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communiry.Entity.Comment
{
    public class CommentEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public int TargetType { get; set; }
        public Guid TargetId { get; set; }
        public Guid? ReplyCommentId { get; set; }
        public Guid? ReplyUserId { get; set; }
        public Guid AuthorId { get; set; }
        public string Content { get; set; }
        public bool IsOffLine { get; set; }
        public DateTime GMTCreate { get; set; }
        public DateTime? GMTModified { get; set; }
    }
}
