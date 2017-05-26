using Community.Contact.Enum;
using Community.Contact.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.IService
{
    public interface ITagService
    {
        Task<GetUserHostTagsResponse> GetHostTags(GetUserHostTags dto);
        Task<GetUserTagsResponse> GetUserTags(GetUserTags dto);
        Task<UpdateUserTagsResponse> UpdateUserTags(UpdateUserTags dto,Guid userId);
    }
}
