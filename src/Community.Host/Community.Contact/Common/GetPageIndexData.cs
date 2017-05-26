using Community.Contact.News;
using Community.Contact.Product;
using Community.Contact.Works;
using System.Collections.Generic;

namespace Community.Contact.Common
{
    public class GetPageIndexData : BaseRequestDto
    {
    }
    public class GetPageIndexDataResponse : BaseRequestDto
    {
        public GetPageIndexDataResponse()
        {
            this.news_list = new List<NewsIntro>();
            this.works_list = new List<SearchWorksListdto>();
            this.product_list = new List<ProductIntro>();
        }
        public List<SearchWorksListdto> works_list { get; set; }
        public List<NewsIntro> news_list { get; set; }
        public List<ProductIntro> product_list { get; set; }
    }
}
