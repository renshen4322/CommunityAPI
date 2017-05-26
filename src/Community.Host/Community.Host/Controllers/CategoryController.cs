using Community.Contact.Category;
using Community.IService;
using Community.OAuth;
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
    public class CategoryController : ApiController
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        
        /// <summary>
        ///获取类型所有分类信息
        /// </summary>
        ///<remarks>
        /// 根据type获取所有分类类目
        /// News:新闻
        /// Product:产品
        /// Works:作品
        /// </remarks>
        /// <returns></returns>        
        [HttpGet]
        [Route("category/search")]
        [ResponseType(typeof(GetTargetAllCategoryResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetTargetAllCategory dto)
        {
           var resp=await _categoryService.GetAllCategoryAsync(dto);
           return Ok(resp);
        }
        /// <summary>
        ///更新对象分类信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("category/object")]
        [ResponseType(typeof(UpdateObjectCategoryResponse))]
        public async Task<IHttpActionResult> Post([FromBody] UpdateObjectCategory dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            var resp = await _categoryService.UpdateCategoryAsync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }
        /// <summary>
        ///获取对象分类信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("category/{object_id}")]
        [ResponseType(typeof(GetObjectCategoryResponse))]
        public async Task<IHttpActionResult> Get([FromUri] GetObjectCategory dto)
        {
            var resp = await _categoryService.GetObjectCategoryByIdAsync(dto);
            return Ok(resp);
        }
    }
}
