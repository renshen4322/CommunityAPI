using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Core.Data;

namespace Communiry.Entity.Group
{
  public class GroupCommentEntity:BaseEntity
    {
      public int Id { get; set; }
      public int PostId { get; set; }

      public int ReplyCommentId { get; set; }
      public Guid ReplyUserId { get; set; }

      public Guid AuthorId { get; set; }

      public string Content { get; set; }
      public bool IsOffLine { get; set; }
      public DateTime GMTCreate { get; set; }

      public DateTime? GMTModified { get; set; }

    }
}
