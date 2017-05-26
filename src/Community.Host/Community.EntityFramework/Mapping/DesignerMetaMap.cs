using Communiry.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.EntityFramework.Mapping
{
    public class DesignerMetaMap : CmEntityTypeConfiguration<DesignerMetaEntity>
    {
        public DesignerMetaMap()
        {
            this.ToTable("community_designer_meta");
            this.HasKey(t =>t.Id);
            this.Property(t => t.CreatedDate).HasColumnName("created_date");
            this.Property(t => t.BaseUserId).HasColumnName("base_user_id");
            this.Property(t => t.DesignAge).HasColumnName("design_age");
       }
    }
}
