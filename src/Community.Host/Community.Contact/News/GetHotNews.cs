using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.News
{
    public class GetHotNews:QueryRequestDto
    {
      
    }
    public class GetHotNewsResponse : QueryRequestDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public string thumbnail_url { get; set; }
        public string introduction { get; set; }
        public DateTime created_date { get; set; }
    }
}
