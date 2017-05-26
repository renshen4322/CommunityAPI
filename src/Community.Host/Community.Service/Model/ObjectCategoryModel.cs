using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Model
{
    public class ObjectCategoryModel : IEqualityComparer<ObjectCategoryModel>
    {
        public int id { get; set; }
        public string name { get; set; }
        public int parent_id { get; set; }
        public string parent_name { get; set; }

        public bool Equals(ObjectCategoryModel x, ObjectCategoryModel y)
        {
            return x.parent_id == y.parent_id;
        }

        public int GetHashCode(ObjectCategoryModel obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
