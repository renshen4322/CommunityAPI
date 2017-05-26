using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communiry.Entity.Group;

namespace Community.EntityFramework.Mapping.Group
{
    public class GroupCommentMap : CmEntityTypeConfiguration<GroupCommentEntity>
    {
        public GroupCommentMap()
        {
            this.ToTable("community_group_comment");
            this.HasKey(t => t.Id);
            this.Property(t => t.PostId).HasColumnName("post_id");
            this.Property(t => t.ReplyCommentId).HasColumnName("reply_comment_id");
            this.Property(t => t.ReplyUserId).HasColumnName("reply_user_id");
            this.Property(t => t.AuthorId).HasColumnName("author_id");
            this.Property(t => t.Content).HasColumnName("content");
            this.Property(t => t.IsOffLine).HasColumnName("is_offline");
            this.Property(t => t.GMTCreate).HasColumnName("gmt_create");
            this.Property(t => t.GMTModified).HasColumnName("gmt_modified");
        
        }
    }
}
