using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Model.Group
{
   public class GroupPostCommentModel
    {
       public Guid AuthorId { get; set; }
       public string AuthorName { get; set; }
       public string AuthorRole { get; set; }
       public string AuthorBio { get; set; }
       public string AuthorAvatar { get; set; }
       public int CommentId { get; set; }
       public string Content { get; set; }
       public int ReplyCommtentId { get; set; }
       public string ReplyCommtnetContent { get; set; }
       public Guid ReplyAuthorId { get; set; }
       public string ReplyAuthorName { get; set; }

       public string ReplyAuthorBio { get; set; }
       public string ReplyAuthorRole { get; set; }
       public string ReplyAuthorAvatar { get; set; }
       public DateTime GMTCreate { get; set; }
    }
}
