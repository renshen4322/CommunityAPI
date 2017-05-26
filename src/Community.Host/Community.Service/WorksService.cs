using AutoMapper;
using Communiry.Entity;
using Communiry.Entity.EnumType;
using Community.Common;
using Community.Common.Exception;
using Community.Contact.Works;
using Community.Core.Data;
using Community.IService;
using Community.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Common.Lucene;
using Community.Service.Const;
using Community.Utils.Common;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Vida.Community.IndexWorker;
using Version = Lucene.Net.Util.Version;


namespace Community.Service
{
    public class WorksService : IWorksService
    {
        #region Fields
        private readonly IRepository<WorksEntity> _worksRepository;
        private readonly IRepository<WorksItemsEntity> _worksItemsRepository;
        private readonly IRepository<WorksMetaEntity> _worksMetaRepository;
        private readonly IRepository<BaseUserEntity> _baseUserRepository;
        private readonly IRepository<WorksQIndexEntity> _worksQIndexRepository;
        private readonly IDapperRepository _dapperRepository;
        #region 根据目录id筛选作品

        readonly string SEARCH_WORKS_SELECT = "select cw.`id` as 'WorksId', cw.`name` as 'Name',"
                                             + " cbu.`nick_name` as 'Author',cui.img_url as 'AvatarUrl',"
                                             + "cbu.`id` as 'AuthorId',cw.introduction as 'Introduction',"
                                             + "cw.thumbnail as 'Thumbnail', cw.created_date as 'CreatedDate' ";

        readonly string SEARCH_WORKS_COUNT = "select count(1) ";
        private const string SEARCH_WORKS_COUNT_FROM = "{0} from community_works as cw "
                                           + "{1}"
                                           + "ORDER BY cw.created_date desc {2} ;";
        private const string SEARCH_WORKS_SELECT_FROM = "select cc.`name` as 'Style',t.* from( {0} from community_works as cw "
                                            + "INNER JOIN community_base_user as cbu "
                                            + "on cw.user_id=cbu.id "
                                            + "LEFT JOIN community_user_images as cui "
                                            + "on cbu.id=cui.user_id and cui.type='Avatar' and cui.is_used=1 "
                                            + "{1}"
                                            + "ORDER BY cw.created_date desc {2} ) as t "
                                            + "LEFT  join community_category_relationships as ccr "
                                            + "on t.WorksId=ccr.object_id "
                                            + "left join community_category as cc "
                                            + "on ccr.category_id=cc.id ORDER BY t.CreatedDate desc ;";

        private const string SEARCH_WORKS_WHERE = "where cw.`id` in (select object_id from community_category_relationships "
                                                    + "where({0}) "
                                                    + "GROUP BY object_id "
                                                    + "HAVING count(object_id)>{1})";
        #endregion

        /// <summary>
        /// 删除作品物品清单
        /// </summary>
        private const string DELETE_ITEMLIST = "delete from community_works_items where works_id=@worksId;";

        /// <summary>
        /// 搜索设计师作品列表
        /// </summary>
        private const string SELECT_DESIGNER_WORKS = "select t.*,cc.name as 'Stype' from "
                                                        + " (select cw.id as 'WorksId',cw.thumbnail as 'Thumbnail', "
                                                        + " cw.name as 'Name',cw.user_id as 'OwnerId', "
                                                        + " cw.introduction as 'Introduction',cw.pano_url as 'PanoUrl' "
                                                        + " ,cw.pano_thumbnail as 'PanoThumbnail',cw.images as 'Images', "
                                                        + " cw.image_thumbnail as 'ImageThumbnail', cw.design_date as 'DesignDate', "
                                                        + " cw.created_date as 'CreatedDate' "
                                                        + "  from community_works as cw "
                                                        + " where cw.user_id=@userId and cw.off_line=0 "
                                                        + " order by cw.created_date desc "
                                                        + " LIMIT @start,@length ) as t "
                                                        + " left join community_category_relationships as ccr "
                                                        + " on t.WorksId=ccr.object_id "
                                                        + " left join community_category as cc "
                                                        + " on ccr.category_id=cc.id and cc.parent_id in ( "
                                                        + " select id from community_category "
                                                        + " where sys_name='Style' and type_id in( "
                                                        + " select id from community_category_type "
                                                        + " where type_name='works')) order by t.CreatedDate desc ;";

        /// <summary>
        /// 根据作品名或用户昵称搜索作品列表
        /// </summary>
        private const string SELECT_WORKS_BY_WORKSNAME_USERNAME_SELECT = "select cw.id as 'WorksId',cw.`name` as 'Name',"
                                                                            + "cbu.nick_name as 'Author',"
                                                                            + "cui.img_url as 'AvatarUrl',cw.user_id as 'AuthorId',"
                                                                            + "cw.introduction as 'Introduction',cw.thumbnail as 'Thumbnail',"
                                                                            + "cw.created_date as 'CreatedDate'";
        private const string SELECT_WORKS_BY_WORKSNAME_USERNAME_COUNT = "select count(1) ";
        private const string SELECT_WORKS_BY_WORKSNAME_USERNAME_FROM = "{0} from community_works as cw "
                                                              + "inner join community_base_user as cbu "
                                                              + "on cw.user_id=cbu.id "
                                                              + "LEFT JOIN community_user_images as cui "
                                                              + "on cw.user_id=cui.user_id and cui.is_used=1 and cui.type='Avatar' "
                                                              + "where(cw.`name` like '%{1}%'|| cbu.nick_name like '%{1}%')&& cw.off_line=0 "
                                                              + "order by cw.created_date desc {2} ;";
        #endregion

        #region Ctor
        public WorksService(IRepository<WorksEntity> worksRepository,
                IRepository<WorksItemsEntity> worksItemsRepository,
                IRepository<WorksMetaEntity> worksMetaRepository,
                IRepository<BaseUserEntity> baseUserRepository,
            IRepository<WorksQIndexEntity> worksQIndexRepository,
                IDapperRepository dapperRepository)
        {
            _worksItemsRepository = worksItemsRepository;
            _worksMetaRepository = worksMetaRepository;
            _worksRepository = worksRepository;
            _baseUserRepository = baseUserRepository;
            _dapperRepository = dapperRepository;
            _worksQIndexRepository = worksQIndexRepository;
        }
        #endregion

        #region Method
        public Task UpdateWorksByIdAsync(UpdateWorks dto, Guid userId)
        {
            return Task.Run(() =>
            {

                var worksEntity = _worksRepository.GetById(dto.works_id);
                if (worksEntity != null)
                {
                    if (!worksEntity.UserId.Equals(userId)) throw new UnAuthorizedException("您没有权限修改当前作品!");

                    var worksMetaEntity = _worksMetaRepository.Table.SingleOrDefault(t => t.WorksId.Equals(dto.works_id));
                    if (worksMetaEntity != null)
                    {
                        worksMetaEntity.Cost = dto.cost;
                        worksMetaEntity.ActualArea = dto.actual_area;
                        worksEntity.Name = dto.name;
                        worksEntity.DesignDate = dto.design_date;
                        worksEntity.Thumbnail = dto.thumbnail;
                        worksEntity.Introduction = dto.introduction;
                        worksEntity.PanoUrl = dto.pano_url;
                        worksEntity.PanoThumbnail = dto.pano_thumbnail;
                        worksEntity.Images = dto.images;
                        worksEntity.ImageThumbnail = dto.images_thumbnail;
                        worksEntity.Description = dto.description;
                        _worksRepository.Update(worksEntity);
                        _worksMetaRepository.Update(worksMetaEntity);
                    }
                    _dapperRepository.Execute(DELETE_ITEMLIST, new { worksId = dto.works_id.ToString("D") });
                    if (dto.ItemList == null || !dto.ItemList.Any()) return;
                    var itemsEntity = Mapper.Map<List<WorksItemsEntity>>(dto.ItemList);
                    foreach (var item in itemsEntity)
                    {
                        item.WorksId = dto.works_id;
                    }
                    _worksItemsRepository.Insert(itemsEntity);
                }
                else
                {
                    throw new RequestErrorException("作品不存在!");
                }

            });
        }

        public Task<CreateWorksResponse> CreateWorksAsync(CreateWorks dto, Guid userId)
        {
            return Task.Run(() =>
            {
                var baseUserEntity = _baseUserRepository.TableNoTracking.SingleOrDefault(t => t.Id.Equals(userId));
                if (baseUserEntity == null) throw new RequestErrorException("用户不存在");
                if (dto.owner_id > 0 && !baseUserEntity.UserBaseId.Equals(dto.owner_id)) throw new RequestErrorException("您没有权限导入该作品");

                var worksEntity = Mapper.Map<WorksEntity>(dto);
                var isExist = false;
                if (worksEntity.OriginId != 0 && worksEntity.ImportType.Equals(WorksImportTypeEnum.Import))
                {
                    isExist = _worksRepository.TableNoTracking.Any(t => t.OriginId == worksEntity.OriginId && t.UserId.Equals(userId));
                }
                if (isExist) throw new RequestErrorException("作品重复导入");
                var worksId = Guid.NewGuid();
                worksEntity.UserId = userId;
                worksEntity.Id = worksId;
                worksEntity.CreatedDate = DateTime.Now;
                _worksRepository.Insert(worksEntity);
                var worksMetaEntity = new WorksMetaEntity()
                {
                    ActualArea = dto.actual_area,
                    Cost = dto.cost,
                    WorksId = worksId,
                    IsHot = false

                };
                if (dto.ItemList != null && dto.ItemList.Any())
                {
                    var itemsEntity = Mapper.Map<List<WorksItemsEntity>>(dto.ItemList);
                    foreach (var item in itemsEntity)
                    {
                        item.WorksId = worksId;
                        item.CreatedDate = DateTime.Now;
                    }
                    _worksItemsRepository.Insert(itemsEntity);
                }
                _worksMetaRepository.Insert(worksMetaEntity);
                _worksQIndexRepository.Insert(new WorksQIndexEntity()
                {
                    StateName = StateEnum.Enqueued,
                    CreatedDate = DateTime.Now,
                    worksId = worksId,
                    Type = OptionTypeEnum.Created
                });
                return new CreateWorksResponse() { works_id = worksId };
            });
        }
        /// <summary>
        /// 获取设计师作品列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<GetDesignerWorksListResponse> GetWorksByUserIdAsync(GetDesignerWorksList dto)
        {
            return Task.Run(() =>
            {
                if (_dapperRepository == null) return null;
                var modelList = _dapperRepository.Query<DesignerWorksListModel>(SELECT_DESIGNER_WORKS, new { userId = dto.designer_id.ToString("D"), dto.start, dto.length }).ToList();

                var resp = new GetDesignerWorksListResponse();

                if (modelList.Any())
                {
                    resp.Data = Mapper.Map<List<DesignerWorksInfo>>(CombineWorksByStyle(modelList));

                }
                if (_worksRepository != null)
                    resp.total = _worksRepository.TableNoTracking.Where((t => t.UserId.Equals(dto.designer_id) && !t.OffLine)).Count();
                return resp;
            });
        }


        public Task<GetIpmortWorksIdListResponse> GetImportedWorksOriginIdList(GetIpmortWorksIdList dto, Guid userId)
        {
            return Task.Run(() =>
            {
                GetIpmortWorksIdListResponse resp = new GetIpmortWorksIdListResponse { user_id = userId };
                var list =
                    (from n in
                         _worksRepository.TableNoTracking.Where(t => t.UserId.Equals(userId) && t.OriginId != null)
                     select new
                     {
                         n.CreatedDate,
                         n.OriginId
                     })
                    .ToList()
                    .Select(n => new WorksEntity()
                    {
                        OriginId = n.OriginId,
                        CreatedDate = n.CreatedDate
                    }).ToList();
                resp.works_ids = Mapper.Map<List<IpmortWorksData>>(list);
                return resp;
            });


        }


        public Task<GetWorksResponse> GetWorksByIdAsync(GetWorks dto)
        {

            return Task.Run(() =>
            {
                var worksEntity = _worksRepository.TableNoTracking.SingleOrDefault(t => t.Id.Equals(dto.works_id));
                if (worksEntity == null)
                    throw new NotFoundException(string.Format("不存在id为:{0}的作品!", dto.works_id));
                var metaData =
                    _worksMetaRepository.TableNoTracking.SingleOrDefault(t => t.WorksId.Equals(worksEntity.Id));
                var itemList =
                    _worksItemsRepository.TableNoTracking.Where(t => t.WorksId.Equals(worksEntity.Id)).ToList();
                var resp = Mapper.Map<GetWorksResponse>(worksEntity);
                if (metaData != null)
                {
                    resp.actual_area = metaData.ActualArea;
                    resp.cost = metaData.Cost;
                    resp.helper = metaData.helper;
                }
                if (itemList.Any())
                {
                    Mapper.Map(itemList, resp.products);
                }
                return resp;
            });


        }

        public Task<SearchWorksListResponse> GetWorksListAsync(SearchWorksList dto)
        {

            //  var 
            return Task.Run(() =>
            {


                List<SearchWorksModel> data = null;
                int count = 0;

                if (string.IsNullOrEmpty(dto.type_ids))
                {


                    var sqlData = string.Format(SEARCH_WORKS_SELECT_FROM, SEARCH_WORKS_SELECT, " where cw.off_line = 0 ", string.Format("LIMIT {0},{1}", dto.start, dto.length));
                    var sqlCount = string.Format(SEARCH_WORKS_COUNT_FROM, SEARCH_WORKS_COUNT, " where cw.off_line=0 ", "");
                    LogHelper.Error("sqlData1：" + sqlData);
                    if (_dapperRepository != null)
                    {
                        data = _dapperRepository.Query<SearchWorksModel>(sqlData).ToList();
                        if (data.Any())
                        {
                            count = _dapperRepository.QuerySingleOrDefault<int>(sqlCount);
                        }
                    }
                }
                else
                {
                    var typeidList = dto.type_ids.Split(',');
                    var builder = new StringBuilder();
                    for (var i = 0; i < typeidList.Length; i++)
                    {
                        builder.AppendFormat("category_id= {0}", typeidList[i]);
                        if (i != typeidList.Length - 1)
                        {
                            builder.Append("||");
                        }
                    }
                    var sqlWhere = string.Format(SEARCH_WORKS_WHERE, builder, typeidList.Length - 1) + " and cw.off_line=0 ";
                    var sqlData = string.Format(SEARCH_WORKS_SELECT_FROM, SEARCH_WORKS_SELECT, sqlWhere, string.Format("LIMIT {0},{1}", dto.start, dto.length));
                    var sqlCount = string.Format(SEARCH_WORKS_COUNT_FROM, SEARCH_WORKS_COUNT, sqlWhere, "");
                    if (_dapperRepository != null)
                    {
                        data = _dapperRepository.Query<SearchWorksModel>(sqlData).ToList();
                        if (data.Any())
                        {
                            count = _dapperRepository.QuerySingleOrDefault<int>(sqlCount);
                        }
                    }
                }
                var resp = new SearchWorksListResponse();
                if (data != null)
                {
                    resp.data = Mapper.Map<List<SearchWorksListdto>>(CombineWorksByStyle(data));
                }
                resp.total = count;
                return resp;

            });
        }
        /// <summary>
        /// 根据作品名或用户昵称搜索作品
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<GetWorksListByUNameOrWNameResponse> GetWorksListByUNameOrWNameAsync(GetWorksListByUNameOrWName dto)
        {


            return Task.Run(() =>
            {
                //var resp = new GetWorksListByUNameOrWNameResponse();
                //var indexSearch = LuceneUtils.GetIndexSearcher(WorksConst.INDEX_SEARCH_DIR);
                //var parser = new QueryParser(Version.LUCENE_30, WorksConst.INDEX_WORKS_CONTENT, new StandardAnalyzer(Version.LUCENE_30));
                //var queryString = string.Format("+({0}:{2} {1}:{2})", WorksConst.INDEX_WORKS_AUTHOR, WorksConst.INDEX_WORKS_TITLE, dto.q);
                //var query = parser.Parse(queryString);
                //var sort = new Sort(new SortField(WorksConst.INDEX_WORKS_CREATED_AT, SortField.DOC, true));
                //var docs = LuceneUtils.pageQuery(indexSearch, query, dto.start, dto.length, sort);
                //if (docs != null && docs.Count > 0)
                //{
                //    string[] worksIds = new string[docs.Count];
                //    for (int i = 0; i < docs.Count; i++)
                //    {
                //        worksIds[i] = docs[i].Get(WorksConst.INDEX_WORKS_ID);
                //    }
                //    var list = _dapperRepository.Query<SearchWorksModel>(WorksConst.SELECT_WORKS_INFO_BY_WORKS_ID_LIST, new { worksIds = worksIds, offLine = 0 }).ToList();
                //    if (list.Any())
                //    {
                //        resp.data = Mapper.Map<List<SearchWorksListdto>>(list);
                //        AddStyleLuceneToModel(docs, resp.data);
                //        resp.Total = LuceneUtils.searchTotalRecord(indexSearch, query);
                //    }
                //}

                //return resp;

                #region 原代码
                var resp = new GetWorksListByUNameOrWNameResponse();
                if (_dapperRepository == null) return resp;
                var list = _dapperRepository.Query<SearchWorksModel>(string.Format(SELECT_WORKS_BY_WORKSNAME_USERNAME_FROM, SELECT_WORKS_BY_WORKSNAME_USERNAME_SELECT, dto.q, "LIMIT @start,@length"), new { start = dto.start, length = dto.length }).ToList();
                if (list.Any())
                {
                    resp.data = Mapper.Map<List<SearchWorksListdto>>(list);
                    resp.total = _dapperRepository.ExecuteScalar<int>(string.Format(SELECT_WORKS_BY_WORKSNAME_USERNAME_FROM, SELECT_WORKS_BY_WORKSNAME_USERNAME_COUNT, dto.q, ""));
                }
                else { resp.total = 0; }
                return resp;
                #endregion
            });
        }

        /// <summary>
        /// 删除作品
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="userId"></param>
        /// <exception cref="ForbiddenException">该用户没有权限删除该作品</exception>
        /// <exception cref="RequestErrorException">该作品不存在</exception>
        /// <returns></returns>
        public Task<DeleteWorksResponse> DeleteWorksAsync(DeleteWorks dto, Guid userId)
        {
            return Task.Run(() =>
            {
                var worksEntity = _worksRepository.Table.SingleOrDefault(t => t.Id.Equals(dto.works_id));
                if (worksEntity == null) throw new RequestErrorException("该作品不存在!");
                if (!worksEntity.UserId.Equals(userId)) throw new ForbiddenException("该用户没有权限删除该作品!");

                worksEntity.OffLine = true;
                _worksRepository.Update(worksEntity);
                return new DeleteWorksResponse();

            });
        }
        #endregion

        #region Utilities
        private List<DesignerWorksListModel> CombineWorksByStyle(List<DesignerWorksListModel> data)
        {
            if (data == null) throw new ArgumentNullException("data");
            var designerWorksListModels = (from n in data.Distinct(new DesignerWorksListModel())
                                           select new DesignerWorksListModel
                                           {
                                               CreatedDate = n.CreatedDate,
                                               Style = "",
                                               WorksId = n.WorksId,
                                               OwnerId = n.OwnerId,
                                               DesignDate = n.DesignDate,
                                               Images = n.Images,
                                               Thumbnail = n.Thumbnail,
                                               ImageThumbnail = n.ImageThumbnail,
                                               Introduction = n.Introduction,
                                               Name = n.Name,
                                               PanoThumbnail = n.PanoThumbnail,
                                               PanoUrl = n.PanoUrl
                                           }
            ).ToList();
            foreach (var destItem in designerWorksListModels)
            {
                foreach (var originItem in data)
                {
                    if (destItem != null && destItem.WorksId.Equals(originItem.WorksId))
                    {
                        destItem.Style += originItem.Style + ",";
                    }
                }
                if (destItem != null) destItem.Style = destItem.Style.Trim(new char[] { ',' });
            }
            return designerWorksListModels;
        }

        private List<SearchWorksModel> CombineWorksByStyle(List<SearchWorksModel> data)
        {
            LogHelper.Error("查询总数为：" + data.Count());
            var resu = (from n in data.Distinct(new SearchWorksModel())
                        select new SearchWorksModel
                        {
                            CreatedDate = n.CreatedDate,
                            Style = "",
                            WorksId = n.WorksId,
                            Author = n.Author,
                            Name = n.Name,
                            AvatarUrl = n.AvatarUrl,
                            AuthorId = n.AuthorId,
                            Introduction = n.Introduction,
                            Thumbnail = n.Thumbnail
                        }
            ).ToList();
            foreach (var destItem in resu)
            {
                foreach (var originItem in data)
                {
                    if (destItem.WorksId.Equals(originItem.WorksId))
                    {
                        destItem.Style += originItem.Style + ",";
                    }
                }
                destItem.Style = destItem.Style.Trim(',');
            }
            return resu;
        }

        private void AddStyleLuceneToModel(List<Document> docs, List<SearchWorksListdto> worksModels)
        {
            for (int i = 0; i < docs.Count; i++)
            {
                var worksId = docs[i].Get(WorksConst.INDEX_WORKS_ID);
                var worksStyles = docs[i].Get(WorksConst.INDEX_WORKS_STYLES);
                foreach (SearchWorksListdto model in worksModels)
                {
                    if (model.works_id.ToString("D").Equals(worksId))
                    {
                        model.style = worksStyles;
                        break;
                    }
                }
            }


        }
        #endregion
    }
}
