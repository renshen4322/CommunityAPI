using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communiry.Entity.Group;

namespace Community.EntityFramework.Mapping.Group
{
    public class GroupPostContentMap : CmEntityTypeConfiguration<GroupPostContentEntity>
    {
        public GroupPostContentMap()
        {
            this.ToTable("community_group_post_content");
            this.HasKey(t => t.Id);
            this.Property(t => t.PostId).HasColumnName("post_id");
            this.Property(t => t.Content).HasColumnName("content");
            this.Property(t => t.GMTCreate).HasColumnName("gmt_create");
            this.Property(t => t.GMTModified).HasColumnName("gmt_modified");
        }
    }
}
