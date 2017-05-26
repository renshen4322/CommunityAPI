using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communiry.Entity.Comment;

namespace Community.EntityFramework.Mapping.Comment
{
   public class ResourceLikeCountMap:CmEntityTypeConfiguration<ResourceLikeCountEntity>
    {
       public ResourceLikeCountMap()
       {
           this.ToTable("community_resource_like_count");
           this.HasKey(t => t.Id);
           this.Property(t => t.ResourceType).HasColumnName("resource_type");
           this.Property(t => t.ResourceId).HasColumnName("resource_id");
           this.Property(t => t.Count).HasColumnName("count");
           this.Property(t => t.GMTCreate).HasColumnName("gmt_create");
           this.Property(t => t.GMTModified).HasColumnName("gmt_modified");
        
       }
    }
}
