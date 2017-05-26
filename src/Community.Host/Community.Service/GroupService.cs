using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Community.IService;
using Community.Core.Data;
using Communiry.Entity.Group;
using Community.Common.Exception;
using Community.Contact.Group;
using Community.Service.Const;
using Community.Service.Model.Group;
using Community.Utils.Common;

namespace Community.Service
{
    public class GroupService : IGroupService
    {
        #region Fields
        private readonly IDapperRepository _dapperRepository;
        private readonly IRepository<GroupClassifyEntity> _groupClassifyRepository;
        private readonly IRepository<GroupInfoEntity> _groupInfoRepository;
        private readonly IRepository<GroupPostEntity> _groupPostRepository;
        private readonly IRepository<GroupUserEntity> _groupUserRepository;
        private readonly IRepository<GroupPostContentEntity> _groupPostContentRepository;
        private readonly IRepository<GroupCommentEntity> _groupCommentRepository;

        #endregion

        #region Ctor
        public GroupService(IDapperRepository dapperRepository,
            IRepository<GroupClassifyEntity> groupClassifyRepository,
            IRepository<GroupInfoEntity> groupInfoRepository,
            IRepository<GroupPostEntity> groupPostRepository,
              IRepository<GroupPostContentEntity> groupPostContentRepository,
            IRepository<GroupUserEntity> groupUserRepository,
            IRepository<GroupCommentEntity> groupCommentRepository
            )
        {
            _dapperRepository = dapperRepository;
            _groupClassifyRepository = groupClassifyRepository;
            _groupInfoRepository = groupInfoRepository;
            _groupPostRepository = groupPostRepository;
            _groupUserRepository = groupUserRepository;
            _groupPostContentRepository = groupPostContentRepository;
            _groupCommentRepository = groupCommentRepository;
        }

        #endregion


        #region Method
        public Task<AddCommentResponseDto> AddCommentSync(AddCommentRequestDto dto, Guid userId)
        {
            return Task.Run(() =>
            {

                AddCommentResponseDto resp = new AddCommentResponseDto();
                var postEnity = _groupPostRepository.Table.SingleOrDefault(t => t.Id.Equals(dto.post_id) && !t.IsOffLine);
                if (postEnity != null)
                {
                    var userGroupEntity = _groupUserRepository.TableNoTracking.SingleOrDefault(
         t => t.UserId.Equals(userId) && t.GroupId.Equals(postEnity.GroupId));
                    if (userGroupEntity != null)
                    {
                        dto.content = System.Web.HttpUtility.HtmlEncode(dto.content);
                        GroupCommentEntity commentEntity = new GroupCommentEntity()
                        {
                            AuthorId = userId,
                            Content = dto.content,
                            GMTCreate = DateTime.Now,
                            IsOffLine = false,
                            PostId = dto.post_id,
                            ReplyUserId = Guid.Empty
                        };
                        if (dto.reply_comment_id > 0)
                        {
                            var replyCommentEntity = _groupCommentRepository.TableNoTracking.SingleOrDefault(
                                t => t.Id.Equals(dto.reply_comment_id) && !t.IsOffLine);
                            if (replyCommentEntity != null)
                            {
                                commentEntity.ReplyUserId = replyCommentEntity.AuthorId;
                                commentEntity.ReplyCommentId = replyCommentEntity.Id;
                                _groupCommentRepository.Insert(commentEntity);
                                var postEntity = _groupPostRepository.Table.SingleOrDefault(t => t.Id.Equals(commentEntity.PostId));
                                postEntity.CommentCount += 1;
                                _groupPostRepository.Update(postEnity);
                                resp.count = postEntity.CommentCount;
                                resp.comment_id = commentEntity.Id;
                                resp.result = true;
                                resp.msg = "";
                            }
                            else
                            {
                                resp.result = false;
                                resp.msg = "回复的评论不存在";
                            }
                        }
                        else
                        {
                            _groupCommentRepository.Insert(commentEntity);
                            var postEntity = _groupPostRepository.Table.SingleOrDefault(t => t.Id.Equals(commentEntity.PostId));
                            postEntity.CommentCount += 1;
                            _groupPostRepository.Update(postEnity);
                            resp.count = postEntity.CommentCount;
                            resp.comment_id = commentEntity.Id;
                            resp.result = true;
                            resp.msg = "";
                        }

                    }
                    else
                    {
                        resp.result = false;
                        resp.msg = "未加入小组，无法发表评论";
                    }
                }
                else
                {
                    resp.result = false;
                    resp.msg = "帖子不存在";
                }

                return resp;

            });
        }
        public Task<DeleteGroupCommentResponseDto> DeleteGroupCommentByCommentIdSync(DeleteGroupCommentRequestDto dto, Guid userId)
        {
            return Task.Run(() =>
            {
                DeleteGroupCommentResponseDto resp = new DeleteGroupCommentResponseDto();
                var groupCommentEntity = _groupCommentRepository.Table.SingleOrDefault(t => t.Id.Equals(dto.comment_id));
                if (groupCommentEntity != null)
                {
                    if (groupCommentEntity.AuthorId.Equals(userId))
                    {
                        if (!groupCommentEntity.IsOffLine)
                        {
                            groupCommentEntity.IsOffLine = true;
                            _groupCommentRepository.Update(groupCommentEntity);
                            GroupPostEntity postEntity = _groupPostRepository.Table.SingleOrDefault(t => t.Id.Equals(groupCommentEntity.PostId));
                            postEntity.CommentCount -= 1;
                            _groupPostRepository.Update(postEntity);
                            resp.result = true;
                            resp.msg = "";
                        }
                        else
                        {
                            resp.result = false;
                            resp.msg = "不存在该评论";
                        }

                    }
                    else
                    {
                        resp.result = false;
                        resp.msg = "删除操作被拒绝";
                    }

                }
                else
                {
                    resp.result = false;
                    resp.msg = "评论不存在";
                }

                return resp;
            });
        }
        public Task<AddPostResponseDto> AddPostSync(AddPostRequestDto dto, Guid userId)
        {
            return Task.Run(() =>
            {
                AddPostResponseDto resp = new AddPostResponseDto();
                var userGroupEntity = _groupUserRepository.TableNoTracking.SingleOrDefault(
               t => t.UserId.Equals(userId) && t.GroupId.Equals(dto.group_id));
                if (userGroupEntity != null)
                {
                    var time = DateTime.Now;
                    var postEnity = new GroupPostEntity()
                    {
                        AuthorId = userId,
                        CollectCount = 0,
                        CommentCount = 0,
                        GMTCreate = time,
                        GroupId = dto.group_id,
                        IsOffLine = false,
                        Title = dto.title,
                        LikeCount = 0
                    };
                    _groupPostRepository.Insert(postEnity);
                    _groupPostContentRepository.Insert(new GroupPostContentEntity()
                    {
                        Content = dto.content,
                        GMTCreate = time,
                        PostId = postEnity.Id
                    });
                    resp.result = true;
                    resp.msg = "";
                    resp.post_id = postEnity.Id;
                }
                else
                {
                    resp.result = false;
                    resp.msg = "未加入下组，无法发帖";
                }


                return resp;
            });
        }


        public Task<UpdatePostResponseDto> UpdatePostSync(UpdatePostRequestDto dto, Guid userId)
        {
            return Task.Run(() =>
            {
                UpdatePostResponseDto resp = new UpdatePostResponseDto();

                var postEnity = _groupPostRepository.Table.SingleOrDefault(t => t.Id.Equals(dto.post_id) && !t.IsOffLine);
                if (postEnity != null)
                {
                    var userGroupEntity = _groupUserRepository.TableNoTracking.SingleOrDefault(
         t => t.UserId.Equals(userId) && t.GroupId.Equals(postEnity.GroupId));
                    if (userGroupEntity != null)
                    {
                        if (postEnity.AuthorId.Equals(userId))
                        {
                            var postContentEntity =
                                   _groupPostContentRepository.Table.SingleOrDefault(t => t.PostId.Equals(postEnity.Id));


                            postEnity.Title = dto.title;
                            postEnity.GMTModified = DateTime.Now;
                            postContentEntity.Content = dto.content;
                            postContentEntity.GMTModified = postEnity.GMTModified;

                            _groupPostRepository.Update(postEnity);
                            _groupPostContentRepository.Update(postContentEntity);
                            resp.result = true;
                            resp.msg = "";

                        }
                        else
                        {
                            resp.result = false;
                            resp.msg = "您没有权限修改该帖子";
                        }
                    }
                    else
                    {
                        resp.result = false;
                        resp.msg = "未加入下组，无法更新帖子";
                    }
                }
                else
                {
                    resp.result = false;
                    resp.msg = "帖子不存在";
                }





                return resp;
            });
        }
        public Task<DeletePostResponseDto> DeletePostByPostIdSync(DeletePostRequestDto dto, Guid userId)
        {
            return Task.Run(() =>
            {
                DeletePostResponseDto resp = new DeletePostResponseDto();
                var postEntity = _groupPostRepository.Table.SingleOrDefault(t => t.Id.Equals(dto.post_id) && !t.IsOffLine);
                if (postEntity != null)
                {
                    if (postEntity.AuthorId.Equals(userId))
                    {
                        postEntity.IsOffLine = true;
                        postEntity.GMTModified = DateTime.Now;
                        _groupPostRepository.Delete(postEntity);
                        resp.result = true;
                        resp.msg = "";
                    }
                    else
                    {
                        resp.result = false;
                        resp.msg = "没有权限删除该帖子";
                    }
                }
                else
                {
                    resp.result = false;
                    resp.msg = "帖子不存在";
                }


                return resp;
            });
        }
        public Task<JoinGroupResponseDto> JoinGroupSync(JoinGroupRequestDto dto, Guid userId)
        {
            return Task.Run(() =>
            {
                JoinGroupResponseDto resp = new JoinGroupResponseDto();

                var groupInfoEntity = _groupInfoRepository.TableNoTracking.SingleOrDefault(t => t.Id.Equals(dto.group_id) && !t.IsOffLine);
                if (groupInfoEntity != null)
                {
                    var userGroupEntity = _groupUserRepository.TableNoTracking.SingleOrDefault(
                  t => t.UserId.Equals(userId) && t.GroupId.Equals(dto.group_id));
                    if (userGroupEntity == null)
                    {
                        _groupUserRepository.Insert(new GroupUserEntity()
                        {
                            GroupId = dto.group_id,
                            GMTCreate = DateTime.Now,
                            UserId = userId
                        });
                        resp.msg = "";
                        resp.result = true;
                    }
                    else
                    {
                        resp.msg = "不可重复加入";
                        resp.result = false;
                    }
                }
                else
                {
                    resp.msg = "不存在该小组";
                    resp.result = false;
                }


                return resp;
            });
        }

        public Task<QuitGroupResponseDto> QuitGroupSync(QuitGroupRequestDto dto, Guid userId)
        {
            return Task.Run(() =>
            {
                QuitGroupResponseDto resp = new QuitGroupResponseDto();

                var groupInfoEntity = _groupInfoRepository.TableNoTracking.SingleOrDefault(t => t.Id.Equals(dto.group_id) && !t.IsOffLine);
                if (groupInfoEntity != null)
                {
                    var userGroupEntity = _groupUserRepository.Table.SingleOrDefault(
                  t => t.UserId.Equals(userId) && t.GroupId.Equals(dto.group_id));
                    if (userGroupEntity != null)
                    {
                        _groupUserRepository.Delete(userGroupEntity);
                        resp.msg = "";
                        resp.result = true;
                    }
                    else
                    {
                        resp.msg = "未加入该小组";
                        resp.result = false;
                    }
                }
                else
                {
                    resp.msg = "不存在该小组";
                    resp.result = false;
                }


                return resp;
            });
        }
        public Task<GetClassifyListResponseDto> GetClassifyListSync(GetClassifyListRequestDto dto)
        {
            return Task.Run(() =>
            {
                GetClassifyListResponseDto resp = new GetClassifyListResponseDto();
                var classifyEntities = _groupClassifyRepository.TableNoTracking.Where(t => !t.IsOffLine);
                if (classifyEntities.Any())
                {
                    List<GroupClassifyEntity> list = classifyEntities.ToList();
                    list.Sort((x, y) =>
                    {
                        return x.Order - y.Order;
                    });
                    resp.data = Mapper.Map<List<ClassifyInfoDto>>(list);
                }
                return resp;

            });
        }

        public Task<GetGroupInfoListResponseDto> GetGroupInfoListByClassifyIdSync(GetGroupInfoListRequestDto dto)
        {
            return Task.Run(() =>
            {
                GetGroupInfoListResponseDto resp = new GetGroupInfoListResponseDto();
                var groupInfoEntities = _groupInfoRepository.TableNoTracking.Where(t => t.ClassifyId.Equals(dto.classify_id) && !t.IsOffLine);
                if (groupInfoEntities.Any())
                {
                    List<GroupInfoEntity> list = groupInfoEntities.ToList();
                    list.Sort((x, y) =>
                    {
                        return x.Order - y.Order;
                    });
                    resp.data = Mapper.Map<List<GroupDetailInfoDto>>(list);
                }
                return resp;
            });



        }

        public Task<GetGroupMemberResponseDto> GetGroupMemberListByGroupIdSync(GetGroupMemberRequestDto dto)
        {
            return Task.Run(() =>
            {
                GetGroupMemberResponseDto resp = new GetGroupMemberResponseDto();
                IEnumerable<MemberInfoModel> memberInfoList = _dapperRepository.Query<MemberInfoModel>(GroupConst.SELECT_GROUP_MEMBER_LIST, new { GroupId = dto.group_id });
                if (memberInfoList.Any())
                {
                    var list = memberInfoList.ToList();
                    resp.data = Mapper.Map<List<MemberInfoDto>>(list);
                }
                return resp;
            });
        }

        public Task<GetGroupPostDetailResponseDto> GetPostDetailByPostIdSync(GetGroupPostDetailRequestDto dto)
        {
            return Task.Run(() =>
            {
                GetGroupPostDetailResponseDto resp = new GetGroupPostDetailResponseDto();

                PostDetailModel model =
                    _dapperRepository.Query<PostDetailModel>(GroupConst.SELECT_POST_BY_ID, new { PostId = dto.post_id })
                        .SingleOrDefault();
                if (model != null)
                {
                    resp.post_id = model.PostId;
                    resp.post_title = model.PostTitle;
                    resp.content = model.PostContent;
                    resp.like_count = model.LikeCount;
                    resp.commment_count = model.CommentCount;
                    resp.collect_count = model.CollectCount;
                    resp.created_at = DateTimeHelper.DateTimeToStamp(model.GMTCreate);
                    resp.author.avatar_url = model.AuthorAvatarUrl;
                    resp.author.bio = model.AuthorBio;
                    resp.author.id = model.AuthorId;
                    resp.author.name = model.AuthorName;
                    resp.author.role = model.AuthorRole;
                    resp.group_info.id = model.GroupId;
                    resp.group_info.cover_url = model.GroupCoverUrl;
                    resp.group_info.descripation = model.GroupDesc;
                    resp.group_info.name = model.GroupName;
                    resp.classify_id = model.ClassifyId;
                }
                else
                {
                    throw new NotFoundException("不存在该帖子");
                }
                return resp;
            });
        }

        public Task<GetGroupPostListResponseDto> GetPostListByGroupIdSync(GetGroupPostListRequestDto dto)
        {
            return Task.Run(() =>
            {
                GetGroupPostListResponseDto resp = new GetGroupPostListResponseDto();
                resp.total = _dapperRepository.ExecuteScalar<int>(GroupConst.SELECT_GROUP_POST_COUNT_BY_GROUP_ID,
                      new { GroupId = dto.group_id });
                if (resp.total > 0 && dto.start < resp.total)
                {
                    IEnumerable<PostInfoModel> postInfoModels =
                   _dapperRepository.Query<PostInfoModel>(GroupConst.SELECT_GROUP_POST_LIST_BY_GROUP_ID,
                       new { GroupId = dto.group_id, Start = dto.start, Length = dto.length });

                    if (postInfoModels.Any())
                    {
                        var list = postInfoModels.ToList();
                        foreach (var postInfoModel in list)
                        {
                            resp.data.Add(new PostInfoDto()
                            {
                                post_id = postInfoModel.PostId,
                                group_id = postInfoModel.GroupId,
                                group_name = postInfoModel.GroupName,
                                comment_count = postInfoModel.CommentCount,
                                created_at = DateTimeHelper.DateTimeToStamp(postInfoModel.GMTCreate),
                                title = postInfoModel.PostTitle,
                                author = new GroupAuthor()
                                {
                                    id = postInfoModel.AuthorId,
                                    avatar_url = postInfoModel.AuthorAvatarUrl,
                                    bio = postInfoModel.AuthorBio,
                                    name = postInfoModel.AuthorName,
                                    role = postInfoModel.AuthorRole
                                }
                            });
                        }

                    }
                }
                return resp;
            });
        }

        public Task<GetPostCommentListResponseDto> GetCommentListByPostIdSync(GetPostCommentListRequestDto dto)
        {
            return Task.Run(() =>
            {
                GetPostCommentListResponseDto resp = new GetPostCommentListResponseDto();
                var postEntity = _groupPostRepository.TableNoTracking.Where(t => t.Id.Equals(dto.post_id) && !t.IsOffLine).SingleOrDefault();
                if (postEntity != null)
                {
                    resp.total = _dapperRepository.ExecuteScalar<int>(
                    GroupConst.SELECT_GROUP_POST_COMMENT_COUNT_BY_POST_ID, new { PostId = dto.post_id });
                    if (resp.total > 0 && resp.total > dto.start)
                    {
                        IEnumerable<GroupPostCommentModel> groupPostCommentModelList = _dapperRepository.Query<GroupPostCommentModel>(GroupConst.SELECT_GROUP_POST_COMMENT_BY_POST_ID, new { PostId = dto.post_id, Start = dto.start, Length = dto.length });
                        if (groupPostCommentModelList.Any())
                        {
                            foreach (var groupPostCommentModel in groupPostCommentModelList)
                            {
                                resp.data.Add(new PostComment()
                                {
                                    author = new CommentAuthor()
                                    {
                                        avatar_url = groupPostCommentModel.AuthorAvatar,
                                        bio = groupPostCommentModel.AuthorBio,
                                        id = groupPostCommentModel.AuthorId,
                                        is_org = postEntity.AuthorId.Equals(groupPostCommentModel.AuthorId),
                                        name = groupPostCommentModel.AuthorName,
                                        role = groupPostCommentModel.AuthorRole
                                    },
                                    content = System.Web.HttpUtility.HtmlDecode(groupPostCommentModel.Content),
                                    created_at = DateTimeHelper.DateTimeToStamp(groupPostCommentModel.GMTCreate),
                                    id = groupPostCommentModel.CommentId,
                                    in_reply_to_comment_id = groupPostCommentModel.ReplyCommtentId,
                                    in_reply_to_content = System.Web.HttpUtility.HtmlDecode(groupPostCommentModel.ReplyCommtnetContent),
                                    in_reply_to_user = groupPostCommentModel.ReplyAuthorId.Equals(Guid.Empty) ? null : new CommentAuthor()
                                    {
                                        avatar_url = groupPostCommentModel.ReplyAuthorAvatar,
                                        bio = groupPostCommentModel.ReplyAuthorBio,
                                        id = groupPostCommentModel.ReplyAuthorId,
                                        is_org = postEntity.AuthorId.Equals(groupPostCommentModel.ReplyAuthorId),
                                        name = groupPostCommentModel.ReplyAuthorName,
                                        role = groupPostCommentModel.ReplyAuthorRole
                                    }
                                });
                            }
                        }
                    }
                }
                else
                {
                    throw new NotFoundException("帖子不存在");
                }

                return resp;

            });
        }

        public Task<GetClassifyNewPostListResponseDto> GetClassifyNewPostListByClassifyIdSync(GetClassifyNewPostListRequestDto dto)
        {
            return Task.Run(() =>
            {
                GetClassifyNewPostListResponseDto resp = new GetClassifyNewPostListResponseDto();
                GroupClassifyEntity groupClassifyEntity = _groupClassifyRepository.TableNoTracking
                    .SingleOrDefault(t => !t.IsOffLine && t.Id.Equals(dto.classify_id));
                if (groupClassifyEntity != null)
                {
                    resp.total =
                        _dapperRepository.ExecuteScalar<int>(GroupConst.SELECT_CLASSIFY_POST_COUNT_BY_CLASSIFY_ID,
                            new { ClassifyId = dto.classify_id });
                    if (resp.total > 0 && resp.total > dto.start)
                    {
                        var list = _dapperRepository.Query<PostInfoModel>(
                            GroupConst.SELECT_CLASSIFY_POST_BY_CLASSIFY_ID,
                            new { ClassifyId = dto.classify_id, Start = dto.start, Length = dto.length });
                        if (list.Any())
                        {
                            foreach (PostInfoModel postInfoModel in list)
                            {
                                resp.data.Add(new PostInfoDto()
                                {
                                    post_id = postInfoModel.PostId,
                                    group_id = postInfoModel.GroupId,
                                    group_name = postInfoModel.GroupName,
                                    comment_count = postInfoModel.CommentCount,
                                    created_at = DateTimeHelper.DateTimeToStamp(postInfoModel.GMTCreate),
                                    title = postInfoModel.PostTitle,
                                    author = new GroupAuthor()
                                    {
                                        id = postInfoModel.AuthorId,
                                        avatar_url = postInfoModel.AuthorAvatarUrl,
                                        bio = postInfoModel.AuthorBio,
                                        name = postInfoModel.AuthorName,
                                        role = postInfoModel.AuthorRole
                                    }
                                });
                            }
                        }
                    }
                }
                else
                {
                    throw new NotFoundException("不存在该板块");
                }
                return resp;
            });
        }

        public Task<GetGroupPostListByUserIdResponseDto> GetGroupPostListByUserIdSync(GetGroupPostListByUserIdRequestDto dto, Guid userId)
        {
            return Task.Run(() =>
            {
                GetGroupPostListByUserIdResponseDto resp = new GetGroupPostListByUserIdResponseDto();
                resp.total = _dapperRepository.ExecuteScalar<int>(GroupConst.SELECT_USER_JOINED_GROUP_POST_COUNT, new { UserId = userId.ToString("D") });
                if (resp.total > 0 && resp.total > dto.start)
                {
                    var list = _dapperRepository.Query<PostInfoModel>(
                            GroupConst.SELECT_USER_JOINED_GROUP_POST,
                            new { UserId = userId.ToString("D"), Start = dto.start, Length = dto.length });
                    if (list.Any())
                    {
                        foreach (PostInfoModel postInfoModel in list)
                        {
                            resp.data.Add(new PostInfoDto()
                            {
                                post_id = postInfoModel.PostId,
                                group_id = postInfoModel.GroupId,
                                group_name = postInfoModel.GroupName,
                                comment_count = postInfoModel.CommentCount,
                                created_at = DateTimeHelper.DateTimeToStamp(postInfoModel.GMTCreate),
                                title = postInfoModel.PostTitle,
                                author = new GroupAuthor()
                                {
                                    id = postInfoModel.AuthorId,
                                    avatar_url = postInfoModel.AuthorAvatarUrl,
                                    bio = postInfoModel.AuthorBio,
                                    name = postInfoModel.AuthorName,
                                    role = postInfoModel.AuthorRole
                                }
                            });
                        }
                    }
                }
                return resp;

            });
        }

        public Task<Contact.Group.GetHotPostResponseDto> GetHotPostSync(Contact.Group.GetHotPostRequestDto dto)
        {
            throw new NotImplementedException();
        }
        public Task<GetUserJoinGroupListResponseDto> GetUserJoinedGroupListSync(GetUserJoinGroupListRequestDto dto, Guid userId)
        {
            return Task.Run(() =>
            {
                GetUserJoinGroupListResponseDto resp = new GetUserJoinGroupListResponseDto();
                var list = _dapperRepository.Query<GroupInfoModel>(GroupConst.SELECT_USER_JOINED_GROUP_INFO,
                     new { UserId = userId.ToString("D") });
                if (list.Any())
                {
                    resp.data = Mapper.Map<List<GroupDetailInfoDto>>(list);
                }
                return resp;
            });
        }

        public Task<GetGroupDetialResponseDto> GetGroupDetailByIdSync(GetGroupDetialRequestDto dto)
        {
            return Task.Run(() =>
               {
                   GetGroupDetialResponseDto resp = new GetGroupDetialResponseDto();
                   var groupInfoEntity = _groupInfoRepository.TableNoTracking.SingleOrDefault(t => t.Id.Equals(dto.group_id) && !t.IsOffLine);
                   if (groupInfoEntity != null)
                   {
                       var classifyEntity = _groupClassifyRepository.TableNoTracking.SingleOrDefault(
                             t => t.Id.Equals(groupInfoEntity.ClassifyId));
                       resp.data = Mapper.Map<GroupDetailInfoDto>(groupInfoEntity);
                       if (classifyEntity != null)
                       {
                           resp.data.classify_name = classifyEntity.Name;
                       }

                   }
                   else
                   {
                       throw new NotFoundException("不存在该小组信息");
                   }
                   return resp;
               });
        }
        #endregion

        #region Utilities



        #endregion










    }
}
