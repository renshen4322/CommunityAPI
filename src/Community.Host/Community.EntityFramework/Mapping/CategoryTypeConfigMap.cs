using Communiry.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.EntityFramework.Mapping
{
    public class CategoryTypeConfigMap: CmEntityTypeConfiguration<CategoryTypeConfigEntity>
    {
        public CategoryTypeConfigMap()
        {
            this.ToTable("community_category_type");
            this.HasKey(t =>t.Id);
            this.Property(t => t.TypeName).HasColumnName("type_name");
       }
    }
}
