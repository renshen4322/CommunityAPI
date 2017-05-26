using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communiry.Entity;

namespace Community.EntityFramework.Mapping
{
    class WorksQIndexMap: CmEntityTypeConfiguration<WorksQIndexEntity>
    {
        public WorksQIndexMap()
        {
            this.ToTable("community_works_qindex");
            this.HasKey(t =>t.Id);
            this.Property(t => t.worksId).HasColumnName("works_id");
            this.Property(t => t.DbStateName).HasColumnName("state_name");
            this.Property(t => t.DbType).HasColumnName("type");
            this.Property(t => t.CreatedDate).HasColumnName("created_date");
            this.Property(t => t.UpdatedDate).HasColumnName("updated_date");
            this.Ignore(t => t.Type);
            this.Ignore(t => t.StateName);
       }
    }
}
