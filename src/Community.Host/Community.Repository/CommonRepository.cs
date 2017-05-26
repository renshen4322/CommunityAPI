using Communiry.Entity;
using Community.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Repository
{
    public class CommonRepository : ICommonRepository
    {
        public List<ProvinceEntity> GetAllProvince()
        {
            var list = new List<ProvinceEntity>();
            list.Add(new ProvinceEntity() { Name = "a" });
            list.Add(new ProvinceEntity() { Name = "b" });
            return list;
        }
    }
}
