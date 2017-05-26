using Community.Contact.Product;
using Community.IService;
using Community.OAuth;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Community.Host.Controllers
{
    /// <summary>
    /// 产品相关接口
    /// </summary>
    [RoutePrefix("v1")]
    public class ProductController : ApiController
    {
          private readonly IProductService _productService;
          /// <summary>
          /// 初始化服务接口
          /// </summary>
          /// <param name="productService"></param>
          public ProductController(IProductService productService)
         {
             _productService = productService;
         }

        /// <summary>
        /// 发布一个产品
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("product/upload")]
        [ResponseType(typeof(CreateProductResponse))]
        public async Task<IHttpActionResult> Post([FromBody]CreateProduct dto) {
              var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            var resp = await _productService.CreateProductAsync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }
        /// <summary>
        /// 获取已导入的产品的id列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("product/import/id/list")]
        [ResponseType(typeof(GetImportPorductIdListResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetImportPorductIdList dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            var resp = await _productService.GetImportProductOriginIdListAsync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }
        /// <summary>
        ///通过id获取产品详情
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("product/detail/{product_id}")]
        [ResponseType(typeof(GetProductDetailResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetProductDetail dto)
        {
            var resp = await _productService.GetProductDetailByIdAsync(dto);
            return Ok(resp);
        }
        
        /// <summary>
        ///根据用户id获取其上传的产品列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("product/list/{user_id}")]
        [ResponseType(typeof(GetProductListByUserIdResponse))]
        public async Task<IHttpActionResult> Post([FromUri]GetProductListByUserId dto)
        {
          var resp=  await _productService.GetProductsByUserIdAsync(dto);
          return Ok(resp);
        }
        /// <summary>
        ///根据分类id集合，获取对应产品列表
        ///若参数为空，则默认取时间最晚前20条
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("product/search/type")]
        [ResponseType(typeof(SearchProductByTypeResponse))]
        public async Task<IHttpActionResult> Post([FromUri]SearchProductByType dto)
        {
          var resp=  await _productService.SearchProductByTypeIdsAsync(dto);
          return Ok(resp);
        }
        /// <summary>
        ///更新一个产品
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("product")]
        [ResponseType(typeof(UpdateProductResponse))]
        public async Task<IHttpActionResult> Post([FromBody]UpdateProduct dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
          await _productService.UpdateProductByIdAsync(dto, (Guid)oauthUser.UserId);
            return Ok();
        }

        /// <summary>
        ///根据作者或产品名筛选产品
        /// </summary>
        /// <returns></returns>       
        [HttpGet]      
        [Route("product/search/name")]
        [ResponseType(typeof(GetProductListByUNameOrPNameResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetProductListByUNameOrPName dto)
        {
            var resp =await _productService.GetProductListByUNameOrPNameAsync(dto);
            return Ok(resp);
        }

        /// <summary>
        /// 产品删除
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <response code="200">删除成功</response>
        /// <response code="401">token错误</response>
        /// <response code="403">拒绝，该产品不属于当前操作用户</response>
        /// <response code="500">产品不存在</response>
        [HttpDelete]
        [Route("product")]
        [Authorize]
        [ResponseType(typeof(DeleteProductResponse))]
        public async Task<IHttpActionResult> Delete([FromBody]DeleteProduct dto) {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            await _productService.DeleteProductAsync(dto, (Guid)oauthUser.UserId);
            return Ok();
        }

    }
}
