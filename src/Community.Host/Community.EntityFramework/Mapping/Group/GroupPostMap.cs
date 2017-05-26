using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communiry.Entity.Group;

namespace Community.EntityFramework.Mapping.Group
{
    public class GroupPostMap : CmEntityTypeConfiguration<GroupPostEntity>
    {
        public GroupPostMap()
        {
            this.ToTable("community_group_post");
            this.HasKey(t => t.Id);
            this.Property(t => t.GroupId).HasColumnName("group_id");
            this.Property(t => t.Title).HasColumnName("title");
           
            this.Property(t => t.AuthorId).HasColumnName("author_id");
            this.Property(t => t.IsOffLine).HasColumnName("is_offline");
            this.Property(t => t.LikeCount).HasColumnName("like_count");
            this.Property(t => t.CommentCount).HasColumnName("comment_count");
            this.Property(t => t.CollectCount).HasColumnName("collect_count");
            this.Property(t => t.GMTCreate).HasColumnName("gmt_create");
            this.Property(t => t.GMTModified).HasColumnName("gmt_modified");
        }
    }
}
