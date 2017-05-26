using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communiry.Entity.Group;

namespace Community.EntityFramework.Mapping.Group
{
    public class GroupPostReportMap : CmEntityTypeConfiguration<GroupPostReportEntity>
    {
        public GroupPostReportMap()
        {
            this.ToTable("community_group_post_report");
            this.HasKey(t => t.Id);
            this.Property(t => t.PostId).HasColumnName("post_id");
            this.Property(t => t.ReportAuthorId).HasColumnName("report_author_id");
            this.Property(t => t.ReportReason).HasColumnName("report_reason");
            this.Property(t => t.AuditStatus).HasColumnName("audit_status");
            this.Property(t => t.GMTCreate).HasColumnName("gmt_create");
            this.Property(t => t.GMTModified).HasColumnName("gmt_modified");
        }
    }
}
