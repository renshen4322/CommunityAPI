using Communiry.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.EntityFramework.Mapping
{
   public class ProvinceMap: CmEntityTypeConfiguration<ProvinceEntity>
    {
       public ProvinceMap()
        {
            this.ToTable("province");
            this.HasKey(pr => pr.Id);
        }
    }
}
