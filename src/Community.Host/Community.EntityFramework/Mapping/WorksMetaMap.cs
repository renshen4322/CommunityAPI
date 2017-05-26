using Communiry.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.EntityFramework.Mapping
{
   public class WorksMetaMap:CmEntityTypeConfiguration<WorksMetaEntity>
    {
       public WorksMetaMap() {
           this.ToTable("community_works_meta");
           this.HasKey(t => t.Id);
           this.Property(t => t.WorksId).HasColumnName("works_id");
           this.Property(t => t.Cost).HasColumnName("cost");         
           this.Property(t => t.ActualArea).HasColumnName("actual_area");        
           this.Property(t => t.helper).HasColumnName("helper");
            this.Property(t => t.IsHot).HasColumnName("is_hot");
        }
    }
}
