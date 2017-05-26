using Community.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.EntityFramework
{
    public partial class EfDataProviderManager : BaseDataProviderManager
    {
        public EfDataProviderManager(DataSettings settings)
            : base(settings)
        {
        }

        public override IDataProvider LoadDataProvider()
        {

            var providerName = Settings.DataProvider;
            if (String.IsNullOrWhiteSpace(providerName))
                throw new Exception();
                // throw new NopException("Data Settings doesn't contain a providerName");

            switch (providerName.ToLowerInvariant())
            {
               
                case "mysql":
                    return new MySqlDataProvider();
                default:
                    throw new Exception();
            }
        }

    }
}
