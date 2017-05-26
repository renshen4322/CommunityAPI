using Communiry.Entity.Comment;
using Community.Contact.Comment;
using Community.Core.Data;
using Community.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Community.Service.Const;
using Community.Service.Model.Comment;
using Community.Utils.Common;
using TargetTypeEnum = Community.Contact.Comment.Enum.TargetTypeEnum;
using Communiry.Entity;
using Communiry.Entity.Comment.Enum;
using Community.Common.Exception;
using Community.Contact.Comment.Enum;

namespace Community.Service
{
    public class CommentService : ICommentService
    {
        #region Fields
        private readonly IRepository<BaseUserEntity> _baseUserRepository;
        private readonly IRepository<CommentEntity> _commentRepository;
        private readonly IRepository<CommentLikeEntity> _likeRepository;
        private readonly IRepository<CommentLikeCountEntity> _commentLikeCountRepository;
        private readonly IRepository<ResourceLikeCountEntity> _resourceLikeCountRepository;
        private readonly IDapperRepository _dapperRepository;
        private readonly IRepository<ProductEntity> _pruoductRepository;
        private readonly IRepository<WorksEntity> _worksRepository;
        private readonly IRepository<NewsEntity> _newsRepository;
        private readonly IRepository<CommentReportEntity> _commentReportRepository;
        #endregion

        #region Ctor
        public CommentService(
            IRepository<CommentEntity> commentRepository,
        IDapperRepository dapperRepository,
            IRepository<ProductEntity> pruoductRepository,
             IRepository<NewsEntity> newsRepository,
            IRepository<WorksEntity> worksRepository,
            IRepository<CommentLikeCountEntity> commentLikeCountRepository,
            IRepository<CommentLikeEntity> likeRepository,
            IRepository<ResourceLikeCountEntity> resourceLikeCountRepository,
             IRepository<BaseUserEntity> baseUserRepository,
            IRepository<CommentReportEntity> commentReportRepository)
        {
            _baseUserRepository = baseUserRepository;
            _pruoductRepository = pruoductRepository;
            _commentRepository = commentRepository;
            _dapperRepository = dapperRepository;
            _worksRepository = worksRepository;
            _commentLikeCountRepository = commentLikeCountRepository;
            _likeRepository = likeRepository;
            _newsRepository = newsRepository;
            _resourceLikeCountRepository = resourceLikeCountRepository;
            _commentReportRepository = commentReportRepository;
        }
        #endregion

        #region Method
        public Task<AddCommentResponse> AddCommentAsync(AddCommentDto dto, Guid userId)
        {
            return Task.Run(() =>
            {
               
                var commentEntity = Mapper.Map<CommentEntity>(dto);
                commentEntity.Content = System.Web.HttpUtility.HtmlEncode(commentEntity.Content);
                commentEntity.Id = Guid.NewGuid();
                commentEntity.GMTCreate = DateTime.Now;
                commentEntity.GMTModified = null;
                commentEntity.AuthorId = userId;
                commentEntity.IsOffLine = false;
                if (dto.reply_comment_id != null)
                {
                    var commentModel = _commentRepository.GetById(dto.reply_comment_id);
                    if (commentModel != null)
                    {
                        commentEntity.ReplyUserId = commentModel.AuthorId;
                    }
                }
                _commentRepository.Insert(commentEntity);
                _commentLikeCountRepository.Insert(new CommentLikeCountEntity()
                {
                    CommentId = commentEntity.Id,
                    Count = 0,
                    GMTCreate = commentEntity.GMTCreate,
                    GMTModified = null
                });
                var resp = new AddCommentResponse() { 
                    comment_id = commentEntity.Id ,
                    count=_commentRepository.TableNoTracking.Where(t=>t.TargetId.Equals(dto.target_id)&&!t.IsOffLine).Count()
                };
                return resp;
            });
        }
        public Task<GetCommentResponseDto> GetCommentListAsync(GetCommentRequestDto dto)
        {
            return Task.Run(() =>
            {
                GetCommentResponseDto resp;
                switch (dto.sort_type)
                {

                    case QuerySortTypeEnum.like: resp = GetCommentListSortLike(dto);
                        break;
                    default:
                        resp = GetCommentListSortTime(dto);
                        break;
                }

                return resp;
            });
        }
        public Task<GetUserLikeListResponseDto> GetUserCommentLikesListAsync(GetUserLikeListRequestDto dto, Guid userId)
        {
            return Task.Run(() =>
            {
                var commentIdList = _dapperRepository.Query<Guid>(CommentConst.QUERY_USER_LIKE_COUNT_BY_COMMENT,
                     new { userId, targetId = dto.id, commentId = Guid.Empty }).ToList();
                return new GetUserLikeListResponseDto()
                {
                    target_id = dto.id,
                    likes_list = commentIdList
                };
            });
        }
        public Task<LikeResponseDto> CommentLikeAsync(LikeRequestDto dto, Guid userId)
        {
            return Task.Run(() =>
            {
                if (dto.comment_id == null) throw new RequestErrorException("评论id不能为空");
                CommentLikeEntity likeEntity;
                switch (dto.target_type)
                {

                    case TargetTypeEnum.Works:
                        var worksEntity = _worksRepository.GetById(dto.target_id);
                        if (worksEntity != null)
                        {
                            likeEntity = GetLikeData(dto.target_id, userId, dto.comment_id ?? Guid.Empty);
                        }
                        else
                        {
                            throw new RequestErrorException("作品不存在");
                        }
                        break;

                    case TargetTypeEnum.Product:
                        var productEntity = _pruoductRepository.GetById(dto.target_id);
                        if (productEntity != null)
                        {
                            likeEntity = GetLikeData(dto.target_id, userId, dto.comment_id ?? Guid.Empty);

                        }
                        else
                        {
                            throw new RequestErrorException("产品不存在");
                        }
                        break;

                    case TargetTypeEnum.News:
                        var newsEntity = _newsRepository.GetById(dto.target_id);
                        if (newsEntity != null)
                        {
                            likeEntity = GetLikeData(dto.target_id, userId, dto.comment_id ?? Guid.Empty);

                        }
                        else
                        {
                            throw new RequestErrorException("新闻不存在");
                        }
                        break;
                    default:
                        throw new RequestErrorException("类型异常");
                }

                if (likeEntity == null)
                {
                    _likeRepository.Insert(new CommentLikeEntity()
                    {
                        GMTCreate = DateTime.Now,
                        GMTModified = null,
                        TargetType = (int)dto.target_type,
                        TargetId = dto.target_id,
                        CommentId = dto.comment_id ?? Guid.Empty,
                        UserId = userId

                    });
                    var commentEntity = _commentLikeCountRepository.Table.SingleOrDefault(t => t.CommentId.Equals((Guid)dto.comment_id));
                    if (commentEntity != null)
                    {
                        commentEntity.Count += 1;
                        commentEntity.GMTModified = DateTime.Now;
                        _commentLikeCountRepository.Update(commentEntity);
                    }
                    else
                    {

                        _commentLikeCountRepository.Insert(new CommentLikeCountEntity()
                        {
                            CommentId = (Guid)dto.comment_id,
                            Count = 0,
                            GMTCreate = DateTime.Now,
                            GMTModified = null
                        });
                    }
                }
                return new LikeResponseDto() { is_liked = true };
            });
        }
        public Task<LikeResponseDto> CommentUnLikeAsync(LikeRequestDto dto, Guid userId)
        {
            return Task.Run(() =>
            {
                if (dto.comment_id == null) throw new RequestErrorException("评论id不能为空");
                CommentLikeEntity likeEntity;
                switch (dto.target_type)
                {

                    case TargetTypeEnum.Works:

                    case TargetTypeEnum.Product:

                    case TargetTypeEnum.News:
                        likeEntity = GetLikeData(dto.target_id, userId, dto.comment_id ?? Guid.Empty, true);
                        break;
                    default:
                        throw new RequestErrorException("类型异常");
                }
                if (likeEntity != null)
                {
                    _likeRepository.Delete(likeEntity);
                    var commentEntity = _commentLikeCountRepository.Table.SingleOrDefault(t => t.CommentId.Equals((Guid)dto.comment_id));
                    if (commentEntity != null)
                    {
                        commentEntity.Count -= 1;
                        commentEntity.GMTModified = DateTime.Now;
                        _commentLikeCountRepository.Update(commentEntity);
                    }
                    else
                    {

                        _commentLikeCountRepository.Insert(new CommentLikeCountEntity()
                        {
                            CommentId = (Guid)dto.comment_id,
                            Count = 0,
                            GMTCreate = DateTime.Now,
                            GMTModified = null
                        });
                    }
                }
                return new LikeResponseDto() { is_liked = false };
            });
        }
        public Task<GetResourceLikeCountResponseDto> ResourceLikeNumberAsync(GetResourceLikeCountRequestDto dto)
        {
            return Task.Run(() =>
            {
                switch (dto.target_type)
                {

                    case TargetTypeEnum.Works:
                        var worksEntity = _worksRepository.GetById(dto.target_id);
                        if (worksEntity == null)
                            throw new RequestErrorException("作品不存在异常");

                        break;
                    case TargetTypeEnum.Product:
                        var productEntity = _pruoductRepository.GetById(dto.target_id);
                        if (productEntity == null)
                            throw new RequestErrorException("产品不存在异常");
                        break;
                    default:
                        throw new RequestErrorException("类型异常");
                }
                var resourceLikeCountEntity = _resourceLikeCountRepository.TableNoTracking.SingleOrDefault(t => t.ResourceId.Equals(dto.target_id));
                return new GetResourceLikeCountResponseDto() { count = resourceLikeCountEntity == null ? 0 : resourceLikeCountEntity.Count };
            });
        }
        public Task<GetUserIsLikeAResourceResponseDto> UserIsLikeAResourceAsync(GetUserIsLikeAResourceRequestDto dto, Guid userId)
        {
            return Task.Run(() =>
            {
                switch (dto.target_type)
                {

                    case TargetTypeEnum.Works:
                        var worksEntity = _worksRepository.GetById(dto.target_id);
                        if (worksEntity == null)
                            throw new RequestErrorException("作品不存在异常");

                        break;
                    case TargetTypeEnum.Product:
                        var productEntity = _pruoductRepository.GetById(dto.target_id);
                        if (productEntity == null)
                            throw new RequestErrorException("产品不存在异常");
                        break;
                    default:
                        throw new RequestErrorException("类型异常");
                }
                var commentLikeEntity = GetLikeData(dto.target_id, userId, Guid.Empty);
                return new GetUserIsLikeAResourceResponseDto() { is_like = commentLikeEntity != null };
            });
        }
        public Task<ResourceLikeResponseDto> ResourceLikeAsync(ResourceLikeRequestDto dto, Guid userId)
        {
            return Task.Run(() =>
            {
                CommentLikeEntity likeEntity;
                switch (dto.target_type)
                {

                    case TargetTypeEnum.Works:
                        var worksEntity = _worksRepository.GetById(dto.target_id);
                        if (worksEntity != null)
                        {
                            likeEntity = GetLikeData(dto.target_id, userId, Guid.Empty);
                        }
                        else
                        {
                            throw new RequestErrorException("作品不存在");
                        }
                        break;

                    case TargetTypeEnum.Product:
                        var productEntity = _pruoductRepository.GetById(dto.target_id);
                        if (productEntity != null)
                        {
                            likeEntity = GetLikeData(dto.target_id, userId, Guid.Empty);

                        }
                        else
                        {
                            throw new RequestErrorException("产品不存在");
                        }
                        break;

                    default:
                        throw new RequestErrorException("类型异常");

                }

                if (likeEntity == null)
                {
                    _likeRepository.Insert(new CommentLikeEntity()
                    {
                        GMTCreate = DateTime.Now,
                        GMTModified = null,
                        TargetType = (int)dto.target_type,
                        TargetId = dto.target_id,
                        CommentId = Guid.Empty,
                        UserId = userId

                    });
                    var resEntity = _resourceLikeCountRepository.Table.SingleOrDefault(t => t.ResourceId.Equals(dto.target_id));
                    if (resEntity != null)
                    {
                        resEntity.Count += 1;
                        resEntity.GMTModified = DateTime.Now;
                        _resourceLikeCountRepository.Update(resEntity);
                    }
                    else
                    {
                        _resourceLikeCountRepository.Insert(new ResourceLikeCountEntity()
                        {
                            ResourceId = dto.target_id,
                            ResourceType = (int)dto.target_type,
                            Count = 1,
                            GMTCreate = DateTime.Now,
                            GMTModified = null
                        });
                    }

                }
                return new ResourceLikeResponseDto() { success = 1 };
            });
        }
        public Task<ResourceLikeResponseDto> ResourceUnLikeAsync(ResourceLikeRequestDto dto, Guid userId)
        {
            return Task.Run(() =>
            {
                CommentLikeEntity likeEntity;
                switch (dto.target_type)
                {

                    case TargetTypeEnum.Works:

                    case TargetTypeEnum.Product:
                        likeEntity = GetLikeData(dto.target_id, userId, Guid.Empty, true);
                        break;
                    default:
                        throw new RequestErrorException("类型异常");

                }
                if (likeEntity != null)
                {
                    _likeRepository.Delete(likeEntity);
                    var resEntity = _resourceLikeCountRepository.Table.SingleOrDefault(t => t.ResourceId.Equals(dto.target_id));
                    if (resEntity != null)
                    {
                        resEntity.Count -= 1;
                        resEntity.GMTModified = DateTime.Now;
                        _resourceLikeCountRepository.Update(resEntity);
                    }
                }
                return new ResourceLikeResponseDto() { success = 1 };
            });
        }
        public Task<ReportCommentResponseDto> CommentReportAsync(ReportCommentRequestDto dto, Guid userId)
        {
            return Task.Run(() =>
            {
                var resp = new ReportCommentResponseDto();
                var userEntity = _baseUserRepository.GetById(userId);
                var commentEntity = _commentRepository.TableNoTracking.SingleOrDefault(t => t.Id.Equals(dto.comment_id));
                if (commentEntity != null)
                {
                    var commentReportEntity = _commentReportRepository.TableNoTracking.SingleOrDefault(t => t.ReportAuthorId.Equals(userId));

                    if (commentReportEntity == null)
                    {
                        CommentReportEntity reportEntity = new CommentReportEntity()
                        {
                            CommentId = dto.comment_id,
                            AuditStatus = AuditStatusEnum.pending.ToString(),
                            CommentContent = commentEntity.Content,
                            GMTCreate = DateTime.Now,
                            GMTModified = null,
                            ReportAuthorId = userEntity.Id,
                            ReportAuthorName = userEntity.NickName,
                            ReportReason = dto.report_reason
                        };
                        _commentReportRepository.Insert(reportEntity);
                    }
                    resp.success = 0;
                }
                else
                {
                    resp.success = -1;
                    resp.msg = "评论不存在";
                }

                return resp;
            });
        }
        public Task<DeleteCommentResponseDto> DeleteCommentAsync(DeleteCommentRequestDto dto, Guid userid)
        {
            return Task.Run(() =>
            {
                var commentEntity = _commentRepository.Table.SingleOrDefault(t => t.Id.Equals(dto.comment_id));
                if (commentEntity != null)
                {
                    if (commentEntity.AuthorId.Equals(userid))
                    {
                        commentEntity.IsOffLine = true;
                        _commentRepository.Update(commentEntity);
                    }
                    else
                    {
                        throw new ForbiddenException("删除操作被拒绝!");
                    }

                }
                else
                {
                    throw new NotFoundException("评论不存在!");
                }

                return new DeleteCommentResponseDto();
            });
        }
        #endregion
        #region Utilities

        private GetCommentResponseDto GetCommentListSortLike(GetCommentRequestDto dto)
        {
            Guid? targetUserId;
            switch (dto.target_type)
            {
                case TargetTypeEnum.Works:
                    var worksModel = _worksRepository.GetById(dto.target_id);
                    if (worksModel != null)
                    {
                        targetUserId = worksModel.UserId;
                    }
                    else
                    {
                        throw new RequestErrorException("作品不存在");
                    }
                    break;
                case TargetTypeEnum.Product:
                    var productModel = _worksRepository.GetById(dto.target_id);
                    if (productModel != null)
                    {
                        targetUserId = productModel.UserId;
                    }
                    else
                    {
                        throw new RequestErrorException("产品不存在");
                    }
                    break;
                default:
                    throw new RequestErrorException("类型异常");
            }
            var list = _dapperRepository.Query<CommentInfoModel>(CommentConst.SELECT_COMMENT_LIST_BY_LIKE,
                    new { targetId = dto.target_id, dto.start, dto.length }).ToArray();
            GetCommentResponseDto resp = new GetCommentResponseDto();
            if (list.Any())
            {
                Array.Sort(list,
                    (x, y) => y.likes_count - x.likes_count);
                List<CommentContent> listData = new List<CommentContent>();
                foreach (CommentInfoModel model in list)
                {
                    listData.Add(new CommentContent()
                    {
                        author = new Author()
                        {
                            avatar_url = model.author_avatar,
                            bio = model.author_bio,
                            id = model.author_id,
                            role = model.author_role,
                            is_org = model.author_id.Equals(targetUserId),
                            name = model.author_name
                        },
                        in_reply_to_user = (model.reply_author_id != null) ? new Author()
                        {
                            avatar_url = model.reply_author_avatar,
                            bio = model.reply_author_bio,
                            id = (Guid)model.reply_author_id,
                            role = model.reply_author_role,
                            is_org = model.reply_author_id.Equals(targetUserId),
                            name = model.reply_author_name
                        } : null,
                        id = model.comment_id,
                        content = System.Web.HttpUtility.HtmlDecode(model.content),
                        likes_count = model.likes_count,
                        in_reply_to_comment_id = model.reply_comment_id,
                        in_reply_to_content = System.Web.HttpUtility.HtmlDecode(model.reply_comment_content),
                        created_at = DateTimeHelper.DateTimeToStamp(model.created_at)
                    });
                }
                resp.data = listData;

            }
            resp.total = _dapperRepository.ExecuteScalar<int>(CommentConst.QUERY_COMMENT_COUNT_BY_TARGETID,
                   new { targetId = dto.target_id });
            return resp;
        }

        /// <summary>
        /// 按时间降序，获取评论列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private GetCommentResponseDto GetCommentListSortTime(GetCommentRequestDto dto)
        {
            Guid? targetUserId = null;
            switch (dto.target_type)
            {
                case TargetTypeEnum.Works:
                    var worksModel = _worksRepository.GetById(dto.target_id);
                    if (worksModel != null)
                    {
                        targetUserId = worksModel.UserId;
                    }
                    break;
                case TargetTypeEnum.Product:
                    var productModel = _worksRepository.GetById(dto.target_id);
                    if (productModel != null)
                    {
                        targetUserId = productModel.UserId;
                    }
                    break;
                case TargetTypeEnum.News:
                    break;
                default:
                    throw new RequestErrorException("类型异常");
            }
            var list = _dapperRepository.Query<CommentInfoModel>(CommentConst.SELECT_COMMENT_LIST_BY_TIME,
                    new { targetId = dto.target_id, dto.start, dto.length }).ToArray();
            GetCommentResponseDto resp = new GetCommentResponseDto();
            if (list.Any())
            {
                //Array.Sort(list,
                //    (x, y) => DateTime.Compare(y.created_at, x.created_at));
                List<CommentContent> listData = new List<CommentContent>();
                foreach (CommentInfoModel model in list)
                {
                    listData.Add(new CommentContent()
                    {
                        author = new Author()
                        {
                            avatar_url = model.author_avatar,
                            bio = model.author_bio,
                            id = model.author_id,
                            role = model.author_role,
                            is_org = targetUserId != null && model.author_id.Equals(targetUserId),
                            name = model.author_name
                        },
                        in_reply_to_user = (model.reply_author_id != null) ? new Author()
                        {
                            avatar_url = model.reply_author_avatar,
                            bio = model.reply_author_bio,
                            id = (Guid)model.reply_author_id,
                            role = model.reply_author_role,
                            is_org = targetUserId != null && model.reply_author_id.Equals(targetUserId),
                            name = model.reply_author_name
                        } : null,
                        id = model.comment_id,
                        content = System.Web.HttpUtility.HtmlDecode(model.content),
                        likes_count = model.likes_count,
                        in_reply_to_comment_id = model.reply_comment_id,
                        in_reply_to_content = System.Web.HttpUtility.HtmlDecode(model.reply_comment_content),
                        created_at = DateTimeHelper.DateTimeToStamp(model.created_at)
                    });
                }
                resp.data = listData;

            }
            resp.total = _dapperRepository.ExecuteScalar<int>(CommentConst.QUERY_COMMENT_COUNT_BY_TARGETID,
                   new { targetId = dto.target_id });
            return resp;
        }

        /// <summary>
        /// 获取喜欢数据
        /// </summary>
        /// <param name="targetId">资源id</param>
        /// <param name="userId">用户id</param>
        /// <param name="commentId">评论id</param>
        /// <param name="tracking">是否跟踪对象状态</param>
        /// <returns></returns>
        private CommentLikeEntity GetLikeData(Guid targetId, Guid userId, Guid commentId, bool tracking = false)
        {
            CommentLikeEntity entity;
            IQueryable<CommentLikeEntity> queryableEntities;
            queryableEntities = tracking ? _likeRepository.Table : _likeRepository.TableNoTracking;
            entity = queryableEntities.SingleOrDefault(
                    t =>
                        t.TargetId.Equals(targetId) && (t.CommentId).Equals(commentId) &&
                        t.UserId.Equals(userId));
            return entity;
        }

        #endregion









    }
}
