using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communiry.Entity.Group;

namespace Community.EntityFramework.Mapping.Group
{
    public class GroupClassifyMap : CmEntityTypeConfiguration<GroupClassifyEntity>
    {
        public GroupClassifyMap()
        {
            this.ToTable("community_group_classify");
            this.HasKey(t => t.Id);
            this.Property(t => t.Name).HasColumnName("name");
            this.Property(t => t.IsOffLine).HasColumnName("is_offline");
            this.Property(t => t.Description).HasColumnName("description");
            this.Property(t => t.Order).HasColumnName("order");
            this.Property(t => t.GMTCreate).HasColumnName("gmt_create");
            this.Property(t => t.GMTModified).HasColumnName("gmt_modified");
        
        }
    }
}
