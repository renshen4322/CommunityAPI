using Community.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Contact.Tag;
using Communiry.Entity;
using Community.Core.Data;
using Community.Common.Exception;

namespace Community.Service
{
    public class TagService : ITagService
    {
        #region Fields
        private readonly IRepository<UserTagEntity> _userTagRepository;
        private readonly IRepository<BaseUserEntity> _baseUserRepository;
        private readonly IDapperRepository _dapperRepository;
        private const string SELECT_HOT_TAG = "select name from community_user_tag "
                                                + "where user_role = @userRole "
                                                + "group by name "
                                                + "order by count(1) desc "
                                                + "limit 0,10;";
        private const string DELETE_USER_TAG = "DELETE from community_user_tag where user_id=@userId;";
        #endregion
        #region Ctor
        public TagService(IRepository<UserTagEntity> userTagRepository,
                          IRepository<BaseUserEntity> baseUserRepository,
                          IDapperRepository dapperRepository
            )
        {
            this._userTagRepository = userTagRepository;
            this._baseUserRepository = baseUserRepository;
            this._dapperRepository = dapperRepository;
        }
        #endregion
        #region Method
        public Task<GetUserHostTagsResponse> GetHostTags(GetUserHostTags dto)
        {

            return Task.Run(() =>
            {
                var resp = new GetUserHostTagsResponse();
                var hotList = _dapperRepository.Query<string>(SELECT_HOT_TAG, new { userRole = dto.type.ToString() }).ToList();
                if (hotList != null && hotList.Count() > 0) resp.hot_tags = hotList;

                return resp;
            });

        }

        public Task<GetUserTagsResponse> GetUserTags(GetUserTags dto)
        {
            return Task.Run(() =>
            {
                var resp = new GetUserTagsResponse();
              var userEntity=  _baseUserRepository.GetById(dto.id);
                if (userEntity == null) throw new RequestErrorException("用户不存在");
                var userTags = (from n in _userTagRepository.TableNoTracking.Where(t => t.UserId.Equals(dto.id))
                                select n.Name
                                ).ToList();
                if (userTags!=null&&userTags.Count()>0)
                {
                    resp.tags = userTags;
                }



                return resp;
            });
        }

        public Task<UpdateUserTagsResponse> UpdateUserTags(UpdateUserTags dto,Guid userId)
        {
            return Task.Run(() =>
            {
                var resp = new UpdateUserTagsResponse();
                var tags = dto.tags.Split(new char[] { ' ' });
                if (tags.Count()>0)
                {
                    List<string> userTags = tags.Distinct().ToList();
                    var userEntity = _baseUserRepository.GetById(userId);
                    if (userEntity == null) throw new RequestErrorException("用户不存在");
                    _dapperRepository.Execute(DELETE_USER_TAG, new { userId = userId.ToString("D") });

                    List<UserTagEntity> tagsEntity=new List<UserTagEntity>();
                    DateTime createdDate= DateTime.Now;
                    foreach (var item in userTags)
                    {
                        tagsEntity.Add(new UserTagEntity() { CreatedDate = createdDate, UserRole = userEntity.Role, Name = item, UserId = userId });
                    }
                    _userTagRepository.Insert(tagsEntity);
                }              

                return resp;
            });
        }
        #endregion

    }
}
