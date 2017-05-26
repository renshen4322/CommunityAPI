using Communiry.Entity.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.EntityFramework.Mapping.Comment
{
   public class CommentLikeMap:CmEntityTypeConfiguration<CommentLikeEntity>
    {
       public CommentLikeMap() {
           this.ToTable("community_like");
           this.HasKey(t => t.Id);
           this.Property(t => t.TargetType).HasColumnName("target_type");
           this.Property(t => t.UserId).HasColumnName("user_id");
           this.Property(t => t.TargetId).HasColumnName("target_id");
           this.Property(t => t.CommentId).HasColumnName("comment_id");
           this.Property(t => t.GMTCreate).HasColumnName("gmt_create");
           this.Property(t => t.GMTModified).HasColumnName("mgt_modified");
       }
    }
}
