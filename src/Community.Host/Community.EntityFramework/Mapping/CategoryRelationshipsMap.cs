using Communiry.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.EntityFramework.Mapping
{
   public class CategoryRelationshipsMap: CmEntityTypeConfiguration<CategoryRelationshipsEntity>
    {
       public CategoryRelationshipsMap()
        {
            this.ToTable("community_category_relationships");
            this.HasKey(t =>t.Id);
            this.Property(t => t.CategoryId).HasColumnName("category_id");
            this.Property(t => t.ObjectId).HasColumnName("object_id");
            this.Property(t => t.CreatedDate).HasColumnName("created_date");
       }
    }
}
