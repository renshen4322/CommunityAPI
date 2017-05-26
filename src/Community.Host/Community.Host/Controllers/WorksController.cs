using Community.Contact.Works;
using Community.IService;
using Community.OAuth;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace Community.Host.Controllers
{
     [RoutePrefix("v1")]
    public class WorksController : ApiController
    {
         private IWorksService _worksService;
         public WorksController(IWorksService worksService)
         {
             this._worksService = worksService;
         }
        /// <summary>
        ///上传作品
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("works/upload")]
        [ResponseType(typeof(CreateWorksResponse))]
         public async Task<IHttpActionResult> Post([FromBody]CreateWorks dto)
        {
             var oauthUser = User.Identity as OAuthToken;
            if (oauthUser.UserId == null)
            {
                return Unauthorized();
            }
           var resp= await _worksService.CreateWorksAsync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }
       
        /// <summary>
        ///获取设计师作品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("works/designer")]
        [ResponseType(typeof(GetDesignerWorksListResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetDesignerWorksList dto)
        {
            var resp = await _worksService.GetWorksByUserIdAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        ///获取设计师已经导入的作品id的列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("works/imporeted/list")]
        [ResponseType(typeof(GetIpmortWorksIdListResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetIpmortWorksIdList dto) {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            var resp = await _worksService.GetImportedWorksOriginIdList(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }
        /// <summary>
        ///获取单个作品详细信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("works/{works_id}")]
         [ResponseType(typeof(GetWorksResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetWorks dto)
        {
            var resp = await _worksService.GetWorksByIdAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        ///根据筛选条件获取作品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("works/search/type")]
        [ResponseType(typeof(SearchWorksListResponse))]
        public async Task<IHttpActionResult> Get([FromUri]SearchWorksList dto)
        {
            var resp = await _worksService.GetWorksListAsync(dto);
          
            return Ok(resp);
        }

        /// <summary>
        ///更新一个作品
        /// </summary>
        /// <returns></returns>       
        [HttpPut]
        [Authorize]
        [Route("works")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Put([FromBody]UpdateWorks dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser.UserId == null)
            {
                return Unauthorized();
            }
             await _worksService.UpdateWorksByIdAsync(dto, (Guid)oauthUser.UserId);
            return Ok();
        }

        /// <summary>
        ///根据作者或作品名筛选作品
        /// </summary>
        /// <returns></returns>       
        [HttpGet]
        [Route("works/search/name")]
        [ResponseType(typeof(GetWorksListByUNameOrWNameResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetWorksListByUNameOrWName dto)
        {
           var resp= await _worksService.GetWorksListByUNameOrWNameAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 产品删除
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <response code="200">删除成功</response>
        /// <response code="401">token错误</response>
        /// <response code="403">拒绝，该作品不属于当前操作用户</response>
        /// <response code="500">作品不存在</response>
        [HttpDelete]
        [Route("works")]
        [Authorize]
        [ResponseType(typeof(DeleteWorksResponse))]
        public async Task<IHttpActionResult> Delete([FromBody]DeleteWorks dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            await _worksService.DeleteWorksAsync(dto, (Guid)oauthUser.UserId);
            return Ok();
        }

    }
}
