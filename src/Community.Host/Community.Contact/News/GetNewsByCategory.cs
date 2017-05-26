using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.News
{
    public class GetNewsByCategory : QueryRequestDto
    {
       public string type_ids { get; set; }
    }
    public class GetNewsByCategoryResponse : QueryResponseDto
   {
        public GetNewsByCategoryResponse() {
            data = new List<NewsIntro>();
        }
        public List<NewsIntro> data { get; set; }
   }
    public class NewsIntro {
        public Guid id { get; set; }
        public string style { get; set; }
        public string title { get; set; }
        public string news_url { get; set; }
        public string thumbnail_url { get; set; }
        public string introduction { get; set; }
        public long created_at { get; set; }
    }
}
