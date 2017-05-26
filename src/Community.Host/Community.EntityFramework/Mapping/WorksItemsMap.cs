using Communiry.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.EntityFramework.Mapping
{
   public class WorksItemsMap:CmEntityTypeConfiguration<WorksItemsEntity>
    {
       public WorksItemsMap() {
           this.ToTable("community_works_items");
           this.HasKey(t => t.Id);
           this.Property(t => t.WorksId).HasColumnName("works_id");
           this.Property(t => t.OwierOriginId).HasColumnName("owner_origin_id");
           this.Property(t => t.ProductOriginId).HasColumnName("product_origin_id");
           this.Property(t => t.ImgUrl).HasColumnName("img_url");
           this.Property(t => t.ProductId).HasColumnName("product_id");
           this.Property(t => t.Name).HasColumnName("name");
           this.Property(t => t.ProductId).HasColumnName("product_id");
           this.Property(t => t.Name).HasColumnName("name");
           this.Property(t => t.CreatedDate).HasColumnName("created_date");
           this.Property(t => t.UpdatedDate).HasColumnName("updated_date");
       }
       
    }
}
