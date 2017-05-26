using Communiry.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Community.EntityFramework.Mapping
{
    public class CityMap : CmEntityTypeConfiguration<CityEntity>
    {
       public CityMap()
        {
            this.ToTable("city");
            this.HasKey(ct => ct.Id);

            this.Property(ct => ct.ProvinceId).HasColumnName("province_id");
            this.Property(ct => ct.AreaCode).HasColumnName("area_code");
        }
    }
}
