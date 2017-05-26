using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Model
{
    public class SearchNewsModel : IEqualityComparer<SearchNewsModel>
    {
        public Guid Id { get; set; }
        public string Style { get; set; }
        public string Title { get; set; }
        public string NewsUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Introduction { get; set; }
        public DateTime CreatedDate { get; set; }


        public bool Equals(SearchNewsModel x, SearchNewsModel y)
        {
            return x.Id.Equals(y.Id);
        }

        public int GetHashCode(SearchNewsModel obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
