using Community.Core.Data;

namespace Communiry.Entity
{
  public class DistrictEntity:BaseEntity
    {
      public int Id { get; set; }
      public int CityId { get; set; }
      public string Name { get; set; }
      public string PostCode { get; set; }
    }
}
