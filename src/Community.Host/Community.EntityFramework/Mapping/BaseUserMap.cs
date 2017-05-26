using Communiry.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.EntityFramework.Mapping
{
   public class BaseUserMap: CmEntityTypeConfiguration<BaseUserEntity>
    {
       public BaseUserMap()
        {
            this.ToTable("community_base_user");
            this.HasKey(di =>di.Id);
            this.Property(di => di.UserBaseId).HasColumnName("user_base_id");

            this.Property(di => di.Birthday).HasColumnName("birthday");
            this.Property(di => di.NickName).HasColumnName("nick_name");
            this.Property(di => di.RealName).HasColumnName("real_name");
            this.Property(di => di.Intro).HasColumnName("intro");
            this.Property(di => di.DbGender).HasColumnName("gender");
            this.Property(di => di.Email).HasColumnName("email");
            this.Property(di => di.Phone).HasColumnName("phone");
            this.Property(di => di.AreaCode).HasColumnName("area_code");
            this.Property(di => di.DbRole).HasColumnName("user_role");
            this.Property(di => di.CreatedDate).HasColumnName("created_date");
            this.Ignore(t => t.Role);
            this.Ignore(t => t.Gender);
       }
    }
}
