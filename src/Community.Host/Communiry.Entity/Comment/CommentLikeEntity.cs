using Community.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communiry.Entity.Comment
{
  public  class CommentLikeEntity:BaseEntity
    {
      public int Id { get; set; }
      public int TargetType { get; set; }
      public Guid TargetId { get; set; }
      public Guid CommentId { get; set; }
      public Guid UserId { get; set; }
      public DateTime GMTCreate { get; set; }
      public DateTime? GMTModified { get; set; }
    }
}
