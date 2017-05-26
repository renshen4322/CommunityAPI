using Community.Contact.News;
using Community.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Community.Host.Controllers
{
    [RoutePrefix("v1")]
    public class NewsController : ApiController
    {
         private INewsService _newsService;
         public NewsController(INewsService newsService)
         {
             this._newsService = newsService;
         }
        /// <summary>
        ///获取热门新闻
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //[Route("news/hot")]
        //[ResponseType(typeof(IEnumerable<GetHotNewsResponse>))]
        //public async Task<IHttpActionResult> Get([FromUri] GetHotNews dto)
        //{
        //    return Ok();
        //}
        /// <summary>
        ///按照分类id获取新闻
        ///默认获取最新20条
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("news/category")]
        [ResponseType(typeof(GetNewsByCategoryResponse))]
        public async Task<IHttpActionResult> Get([FromUri] GetNewsByCategory dto)
        {
            var resp = await _newsService.GetNewsByCategoryIdsAsync(dto);
            return Ok(resp);
        }
    }
}
