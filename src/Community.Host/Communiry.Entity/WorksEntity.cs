using Communiry.Entity.EnumType;
using Community.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communiry.Entity
{
    public class WorksEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public WorksImportTypeEnum ImportType
        {
            get
            {
                return (WorksImportTypeEnum)Enum.Parse(typeof(WorksImportTypeEnum), DbImportType, true);
            }
            set
            {
                this.DbImportType = value.ToString();
            }
        }
        public DateTime DesignDate { get; set; }
        public string Introduction { get; set; }
        public string Description { get; set; }  
        public string DbImportType { get; set; }
        public Guid UserId { get; set; }
        public int? OriginId { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string PanoUrl { get; set; }
        public string PanoThumbnail { get; set; }
        public string Images { get; set; }
        public string ImageThumbnail { get; set; }
        public bool OffLine { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
