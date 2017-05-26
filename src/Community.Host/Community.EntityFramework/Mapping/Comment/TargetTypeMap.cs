using Communiry.Entity.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.EntityFramework.Mapping.Comment
{
   public class TargetTypeMap:CmEntityTypeConfiguration<TargetTypeEntity>
    {
       public TargetTypeMap() {
           this.ToTable("community_target_type");
           this.HasKey(t => t.Id);
           this.Property(t => t.Name).HasColumnName("name");
           this.Property(t => t.Value).HasColumnName("value");
       }
    }
}
