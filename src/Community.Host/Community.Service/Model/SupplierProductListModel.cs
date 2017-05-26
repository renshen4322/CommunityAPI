using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Model
{
   public class SupplierProductListModel: IEqualityComparer<SupplierProductListModel>
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
        public string Introduction { get; set; }
        public string Images { get; set; }
        public string ImageThumbnail { get; set; }
        public string Style { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool Equals(SupplierProductListModel x, SupplierProductListModel y)
        {
            return x.ProductId.Equals(y.ProductId);
        }

        public int GetHashCode(SupplierProductListModel obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
