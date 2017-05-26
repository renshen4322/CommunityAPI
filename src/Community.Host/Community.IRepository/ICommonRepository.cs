using Communiry.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.IRepository
{
    public interface ICommonRepository
    {
        List<ProvinceEntity> GetAllProvince();
    }
}
