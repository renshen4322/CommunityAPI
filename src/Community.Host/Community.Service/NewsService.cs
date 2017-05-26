using AutoMapper;
using Communiry.Entity;
using Community.Contact.News;
using Community.Core.Data;
using Community.IService;
using Community.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service
{
    public class NewsService : INewsService
    {
        #region Fields
        private readonly IRepository<NewsEntity> _newsRepository;
        private readonly IDapperRepository _dapperRepository;

        #region sql
        #region 根据分类筛选新闻
        private const string SELECT_NEW_INFO_COUNT_SELECT = "select count(1) ";

        private const string SELECT_NEW_INFO_SELECT = "select cc.name as 'Style',cn.id as 'Id', cn.title as 'Title',cn.news_url as 'NewsUrl',cn.introduction as 'Introduction',cn.thumbnail as 'ThumbnailUrl',cn.created_date as 'CreatedDate'";

        private const string SEARCH_NEW_SELECT_FROM = "{0} from community_news as cn "
                                                +"LEFT  join community_category_relationships as ccr "
                                                +"on cn.id=ccr.object_id "
                                                +"left join community_category as cc "
                                                +"on ccr.category_id=cc.id "
                                                + "{1}"
                                                + "ORDER BY cn.created_date desc {2}";
        private const string SEARCH_NEW_COUNT_FROM = "{0} from community_news as cn "                                              
                                               + "{1}";
        private const string SEARCH_NEW_WHERE = "where cn.id in (select object_id from community_category_relationships "
                                                    + "where ({0}) "
                                                    + "GROUP BY object_id "
                                                    + "HAVING count(object_id)>{1}) and cn.off_line=0 ";
        #endregion
        #endregion
        #endregion
        #region Ctor
        public NewsService(IRepository<NewsEntity> newsRepository,
                           IDapperRepository dapperRepository)
        {
            this._newsRepository = newsRepository;
            this._dapperRepository = dapperRepository;
        }
        #endregion

        #region Method
        public Task<GetNewsByCategoryResponse> GetNewsByCategoryIdsAsync(GetNewsByCategory dto)
        {
            return Task.Run(() =>
            {
                List<SearchNewsModel> data;
                int count = 0;
                if (string.IsNullOrEmpty(dto.type_ids))
                {
                    var sqlData = string.Format(SEARCH_NEW_SELECT_FROM, SELECT_NEW_INFO_SELECT, " where cn.off_line=0 ", string.Format("LIMIT {0},{1}", dto.start, dto.length));
                    data = _dapperRepository.Query<SearchNewsModel>(sqlData).ToList();
                    if (data != null && data.Count() > 0)
                    {
                        var sqlCount = string.Format(SEARCH_NEW_COUNT_FROM, SELECT_NEW_INFO_COUNT_SELECT, " where cn.off_line=0 ");
                        count = _dapperRepository.QuerySingleOrDefault<int>(sqlCount);
                    }
                }
                else
                {
                    string[] typeidList = dto.type_ids.Split(',');
                    var builder = new StringBuilder();
                    for (int i = 0; i < typeidList.Length; i++)
                    {
                        builder.Append("category_id= " + typeidList[i]);
                        if (i != typeidList.Length - 1)
                        {
                            builder.Append("||");
                        }
                    }
                    var sqlWhere = string.Format(SEARCH_NEW_WHERE, builder.ToString(), typeidList.Length - 1);
                    var sqlData = string.Format(SEARCH_NEW_SELECT_FROM, SELECT_NEW_INFO_SELECT, sqlWhere, string.Format("LIMIT {0},{1}", dto.start, dto.length));

                    data = _dapperRepository.Query<SearchNewsModel>(sqlData).ToList();
                    if (data != null && data.Count() > 0)
                    {
                        var sqlCount = string.Format(SEARCH_NEW_COUNT_FROM, SELECT_NEW_INFO_COUNT_SELECT, sqlWhere);
                        count = _dapperRepository.QuerySingleOrDefault<int>(sqlCount);
                    }
                }
                GetNewsByCategoryResponse resp = new GetNewsByCategoryResponse();
                resp.data = Mapper.Map<List<NewsIntro>>(CombineWorksByStyle(data));
                resp.total = count;
                return resp;

            });
        }
        #endregion

        #region Utilities
        public List<SearchNewsModel> CombineWorksByStyle(List<SearchNewsModel> data)
        {
            List<SearchNewsModel> resu = new List<SearchNewsModel>();
            resu = (from n in data.Distinct(new SearchNewsModel())
                    select new SearchNewsModel
                    {
                        Id = n.Id,
                        Style = "",
                        Title = n.Title,
                        NewsUrl = n.NewsUrl,
                        ThumbnailUrl = n.ThumbnailUrl,
                        Introduction = n.Introduction,
                        CreatedDate = n.CreatedDate
                       
                    }
                       ).ToList();
            foreach (var destItem in resu)
            {
                foreach (var originItem in data)
                {
                    if (destItem.Id.Equals(originItem.Id))
                    {
                        destItem.Style += originItem.Style + ",";
                    }
                }
                destItem.Style = destItem.Style.Trim(new char[] { ',' });
            }
            return resu;
        }

        #endregion
    }
}

