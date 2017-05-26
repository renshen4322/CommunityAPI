using Communiry.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.EntityFramework.Mapping
{
   public class CategoryMap: CmEntityTypeConfiguration<CategoryEntity>
    {
       public CategoryMap()
        {
            this.ToTable("community_category");
            this.HasKey(t =>t.Id);
            this.Property(t => t.TypeId).HasColumnName("type_id");
            this.Property(t => t.ParentId).HasColumnName("parent_id");
            this.Property(t => t.Name).HasColumnName("name");
            this.Property(t => t.OffLine).HasColumnName("off_line");
            this.Property(t => t.IsHot).HasColumnName("is_hot");
            this.Property(t => t.EnName).HasColumnName("en_name");
            this.Property(t => t.IsSystemCategory).HasColumnName("is_system_category");
            this.Property(t => t.SysName).HasColumnName("sys_name");
            this.Property(t => t.DisplayIndex).HasColumnName("display_index");
            this.Property(t => t.CreatedDate).HasColumnName("created_date");
            this.Property(t => t.IsMultiple).HasColumnName("is_multiple");
        }
    }
}
