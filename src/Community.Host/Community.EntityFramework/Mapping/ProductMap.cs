using Communiry.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.EntityFramework.Mapping
{
    public class ProductMap : CmEntityTypeConfiguration<ProductEntity>
    {
        public ProductMap() {
            this.ToTable("community_product");
            this.HasKey(t=>t.Id);
            this.Property(t => t.OriginId).HasColumnName("origin_id");
            this.Property(t => t.UserId).HasColumnName("user_id");
            this.Property(t => t.DbImportType).HasColumnName("import_type");
            this.Property(t => t.Name).HasColumnName("name");
            this.Property(t => t.Thumbnail).HasColumnName("thumbnail");
            this.Property(t => t.Images).HasColumnName("images");
            this.Property(t => t.ImageThumbnail).HasColumnName("image_thumbnail");
            this.Property(t => t.CreatedDate).HasColumnName("created_date");
            this.Property(t => t.Cost).HasColumnName("cost");
            this.Property(t => t.Introduction).HasColumnName("introduction");
            this.Property(t => t.Description).HasColumnName("description");
            this.Property(t => t.OffLine).HasColumnName("off_line");
            this.Ignore(t => t.ImportType);
        }
    }
}
