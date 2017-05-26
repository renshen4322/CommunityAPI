using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communiry.Entity.Group;

namespace Community.EntityFramework.Mapping.Group
{
    public class GroupCommentListMap : CmEntityTypeConfiguration<GroupCommentListEntity>
    {
        public GroupCommentListMap()
        {
            this.ToTable("community_group_comment_like");
            this.HasKey(t => t.Id);
            this.Property(t => t.CommentId).HasColumnName("comment_id");
            this.Property(t => t.GMTCreate).HasColumnName("gmt_create");
            this.Property(t => t.GMTModified).HasColumnName("gmt_modified");
        }
    }
}
