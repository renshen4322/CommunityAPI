using Communiry.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.EntityFramework.Mapping
{
  public  class DistrictMap: CmEntityTypeConfiguration<DistrictEntity>
    {
      public DistrictMap()
        {
            this.ToTable("district");
            this.HasKey(di =>di.Id);
            this.Property(di => di.CityId).HasColumnName("city_id");
            this.Property(di => di.PostCode).HasColumnName("post_code");
        }
    }
}
