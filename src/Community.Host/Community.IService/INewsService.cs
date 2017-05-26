using Community.Contact.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.IService
{
    public interface INewsService
    {
        Task<GetNewsByCategoryResponse> GetNewsByCategoryIdsAsync(GetNewsByCategory dto);

    }
}
