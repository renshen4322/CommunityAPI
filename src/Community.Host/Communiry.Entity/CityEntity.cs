using Community.Core.Data;

namespace Communiry.Entity
{
   public class CityEntity:BaseEntity
    {
       public int Id { get; set; }
       public int ProvinceId { get; set; }
       public string Name { get; set; }
       public string AreaCode { get; set; }
    }
}
