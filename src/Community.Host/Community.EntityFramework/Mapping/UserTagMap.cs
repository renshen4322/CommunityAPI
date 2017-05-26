using Communiry.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.EntityFramework.Mapping
{
   public class UserTagMap : CmEntityTypeConfiguration<UserTagEntity>
    {
        public UserTagMap()
        {
            this.ToTable("community_user_tag");
            this.HasKey(t => t.Id);
            this.Property(t => t.Name).HasColumnName("name");
            this.Property(t => t.UserId).HasColumnName("user_id");
            this.Property(t => t.DbUserRole).HasColumnName("user_role");
            this.Property(t => t.CreatedDate).HasColumnName("created_date");
            this.Ignore(t => t.UserRole);

        }
    }
}
