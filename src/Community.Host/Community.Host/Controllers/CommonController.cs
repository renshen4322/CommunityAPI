using Community.Common;
using Community.Contact.Common;
using Community.Contact.News;
using Community.IService;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Community.Utils.Common;

namespace Community.Host.Controllers
{
    /// <summary>
    /// 通用接口
    /// </summary>
    [RoutePrefix("v1")]
    public class CommonController : ApiController
    {
        private readonly ICommonService _commonService;
        private readonly INewsService _newsService;
        private readonly IProductService _productService;
        private readonly IWorksService _worksService;
     
        /// <summary>
        /// 初始化service构造函数
        /// </summary>
        /// <param name="commonService">通用服务</param>
        /// <param name="newsService">新闻服务</param>
        /// <param name="productService">产品服务</param>
        /// <param name="worksService">作品服务</param>
        public CommonController(ICommonService commonService,
                                INewsService newsService,
                                IProductService productService,
                                IWorksService worksService)
        {
            _commonService = commonService;
            _newsService = newsService;
            _productService = productService;
            _worksService = worksService;
        }
        /// <summary>
        /// 获取所有省份
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("province")]
        [ResponseType(typeof(List<GetProvinceResponse>))]
        public async Task<IHttpActionResult> Get([FromUri]GetProvince dto) {
            var resp = await _commonService.GetProvinceAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 根据省份获取所有城市
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("city")]
        [ResponseType(typeof(List<GetCityResponse>))]
        public async Task<IHttpActionResult> Get([FromUri]GetCity  dto)
        {
            var resp = await _commonService.GetCityAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 根据城市获取所有区县
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("district")]
        [ResponseType(typeof(List<GetDistrictResponse>))]
        public async Task<IHttpActionResult> Get([FromUri]GetDistrict dto)
        {
            var resp = await _commonService.GetDistrictAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 全局搜索
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("search")]
        [ResponseType(typeof(GetAllResourceResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetAllResource dto)
        {
            var resp = await _commonService.GetAllResourceAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 聚合资源
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("pageindex/data")]
        [ResponseType(typeof(GetPageIndexDataResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetPageIndexData dto)
        {
            string cacheKey = "INDEX_ALL_DATA";
            GetPageIndexDataResponse resp = CacheHelper.Get<GetPageIndexDataResponse>(cacheKey);
            if (resp==null)
            {
                resp = new GetPageIndexDataResponse();
                var newsData = await _newsService.GetNewsByCategoryIdsAsync(new GetNewsByCategory() { start = 0, length = 6 });
                var productData = await _productService.SearchProductByTypeIdsAsync(new Contact.Product.SearchProductByType() { start = 0, length = 6 });
                var worksData = await _worksService.GetWorksListAsync(new Contact.Works.SearchWorksList() { start = 0, length = 6 });
                resp.news_list = newsData.data;
                resp.product_list = productData.data;
                resp.works_list = worksData.data;
                CacheHelper.Insert("INDEX_ALL_DATA", resp, 30);   
            }          
            return Ok(resp);
        }
    }

   
}
