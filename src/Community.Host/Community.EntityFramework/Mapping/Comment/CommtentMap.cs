using Communiry.Entity.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.EntityFramework.Mapping.Comment
{
   public class CommtentMap:CmEntityTypeConfiguration<CommentEntity>
    {
       public CommtentMap() {
           this.ToTable("community_comment");
           this.HasKey(t => t.Id);
           this.Property(t => t.TargetType).HasColumnName("target_type");
           this.Property(t => t.TargetId).HasColumnName("target_id");
           this.Property(t => t.ReplyCommentId).HasColumnName("reply_comment_id");
           this.Property(t => t.ReplyUserId).HasColumnName("reply_user_id");
           this.Property(t => t.Content).HasColumnName("content");
           this.Property(t => t.AuthorId).HasColumnName("author_id");
           this.Property(t => t.IsOffLine).HasColumnName("is_offline");
           this.Property(t => t.GMTCreate).HasColumnName("gmt_create");
           this.Property(t => t.GMTModified).HasColumnName("gmt_modified");
       }
    }
}
