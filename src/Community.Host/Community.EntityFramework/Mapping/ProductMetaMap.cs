using Communiry.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.EntityFramework.Mapping
{
  public class ProductMetaMap:CmEntityTypeConfiguration<ProductMetaEntity>
    {
      public ProductMetaMap() {
          this.ToTable("community_product_meta");
          this.HasKey(t => t.Id);
          this.Property(t => t.PruductId).HasColumnName("product_id");
            this.Property(t => t.IsHot).HasColumnName("is_hot");


        }
    }
}
