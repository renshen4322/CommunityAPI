using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communiry.Entity.Group;

namespace Community.EntityFramework.Mapping.Group
{
    public class GroupInfoMap : CmEntityTypeConfiguration<GroupInfoEntity>
    {
        public GroupInfoMap()
        {
            this.ToTable("community_group_info");
            this.HasKey(t => t.Id);
            this.Property(t => t.Name).HasColumnName("name");
            this.Property(t => t.CreatedUser).HasColumnName("created_user");
            this.Property(t => t.ClassifyId).HasColumnName("classify_id");
            this.Property(t => t.Description).HasColumnName("description");
            this.Property(t => t.IsOffLine).HasColumnName("is_offline");
            this.Property(t => t.CoverUrl).HasColumnName("cover_url");
            this.Property(t => t.Order).HasColumnName("order");
            this.Property(t => t.IsHot).HasColumnName("is_hot");
            this.Property(t => t.GMTCreate).HasColumnName("gmt_create");
            this.Property(t => t.GMTModified).HasColumnName("gmt_modified");
        }
    }
}
