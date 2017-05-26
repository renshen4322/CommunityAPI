using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communiry.Entity;

namespace Community.EntityFramework.Mapping
{

    public class SupplierMetaMap : CmEntityTypeConfiguration<SupplierMetaEntity>
    {
        public SupplierMetaMap()
        {
            this.ToTable("community_supplier_meta");
            this.HasKey(t => t.Id);
            this.Property(t => t.CreatedDate).HasColumnName("created_date");
            this.Property(t => t.BaseUserId).HasColumnName("base_user_id");
            this.Property(t => t.Moblie).HasColumnName("mobile");
        }
    }
}
