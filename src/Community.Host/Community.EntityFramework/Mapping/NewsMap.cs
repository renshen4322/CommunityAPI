using Communiry.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.EntityFramework.Mapping
{
   public class NewsMap:CmEntityTypeConfiguration<NewsEntity>
    {
       public NewsMap() {
           this.ToTable("community_news");
           this.HasKey(t => t.Id);
           this.Property(t => t.Title).HasColumnName("title");
           this.Property(t => t.NewsUrl).HasColumnName("news_url");
           this.Property(t => t.Thumbnail).HasColumnName("thumbnail");
           this.Property(t => t.Introduction).HasColumnName("introduction");
           this.Property(t => t.OffLine).HasColumnName("off_line");
           this.Property(t => t.CreatedDate).HasColumnName("created_date");
           this.Property(t => t.IsHot).HasColumnName("is_hot");
         
       }
    }
}
