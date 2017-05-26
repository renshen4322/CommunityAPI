using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Model
{
    public class DesignerWorksListModel : IEqualityComparer<DesignerWorksListModel>
    {
       public string Style { get; set; }
        public Guid WorksId { get; set; }
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
        public string Introduction { get; set; }
        public string PanoUrl { get; set; }
        public string Thumbnail { get; set; }
        public string PanoThumbnail { get; set; }
        public string Images { get; set; }
        public string ImageThumbnail { get; set; }
        public DateTime DesignDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool Equals(DesignerWorksListModel x, DesignerWorksListModel y)
        {
            return x.WorksId.Equals(y.WorksId);
        }

        public int GetHashCode(DesignerWorksListModel obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
