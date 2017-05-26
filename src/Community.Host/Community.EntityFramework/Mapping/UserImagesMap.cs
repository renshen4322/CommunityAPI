using Communiry.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Community.EntityFramework.Mapping
{
    public class UserImagesMap : CmEntityTypeConfiguration<UserImagesEntity>
    {
        public UserImagesMap()
        {
            this.ToTable("community_user_images");
            this.HasKey(ui => ui.Id);
            this.Property(ui => ui.UserId).HasColumnName("user_id");
            this.Property(ui => ui.ImgUrl).HasColumnName("img_url");
            this.Property(ui => ui.IsUsed).HasColumnName("is_used");
            this.Property(ui => ui.CreatedDate).HasColumnName("created_date");
            this.Property(ui => ui.DbType).HasColumnName("type");
            this.Ignore(ui => ui.Type);
        }
    }
}
