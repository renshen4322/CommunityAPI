using Communiry.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.EntityFramework.Mapping
{
    public class AddressMap: CmEntityTypeConfiguration<AddressEntity>
    {
        public AddressMap()
        {
            this.ToTable("community_address");
            this.HasKey(t =>t.Id);
            this.Property(t => t.UserId).HasColumnName("user_id");
            this.Property(t => t.DbType).HasColumnName("type");
            this.Property(t => t.CountryId).HasColumnName("country_id");
            this.Property(t => t.ProvinceId).HasColumnName("province_id");
            this.Property(t => t.CityId).HasColumnName("city_id");
            this.Property(t => t.DistrictId).HasColumnName("district_id");
            this.Property(t => t.CreatedDate).HasColumnName("created_date");
            this.Property(t => t.Street).HasColumnName("street");
            this.Ignore(t => t.Type);
       }
    }
}
