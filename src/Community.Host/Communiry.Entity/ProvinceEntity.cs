using Community.Core.Data;

namespace Communiry.Entity
{
   public class ProvinceEntity:BaseEntity
    {
       public int Id { get; set; }
       public string Name { get; set; }
       public int OrderId { get; set; }
    }
}
