using Community.Contact.Tag;
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
    public class TagController : ApiController
    {
        private ITagService _tagService;
        public TagController(ITagService tagService)
        {
            this._tagService = tagService;
        }
        /// <summary>
        ///获取热门标签
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("tag/hot")]
        [ResponseType(typeof(GetUserHostTagsResponse))]
        public async Task<IHttpActionResult> Get([FromUri] GetUserHostTags dto)
        {
            var resp = await _tagService.GetHostTags(dto);
            return Ok(resp);
        }
        /// <summary>
        ///获取用户自定义标签
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("tag/user")]
        [ResponseType(typeof(GetUserTagsResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetUserTags dto)
        {
            var resp = await _tagService.GetUserTags(dto);
            return Ok(resp);
        }
        /// <summary>
        ///修改用户自定义标签
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("tag/user")]
        [ResponseType(typeof(UpdateUserTagsResponse))]
        public async Task<IHttpActionResult> Post([FromBody]UpdateUserTags dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            var resp = await _tagService.UpdateUserTags(dto,(Guid)oauthUser.UserId);
            return Ok(resp);
        }
    }
}
