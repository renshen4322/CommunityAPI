using Communiry.Entity.EnumType;
using Community.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communiry.Entity
{
   public  class UserImagesEntity:BaseEntity
    {
       public int Id { get; set; }
       public Guid UserId { get; set; }
      
       public ImageTypeEnum Type
       {
           get
           {

               return (ImageTypeEnum)Enum.Parse(typeof(ImageTypeEnum), DbType, true);
           }
           set { DbType = value.ToString(); }
       }
       public string DbType { get; set; }
       public string ImgUrl { get; set; }
       public bool IsUsed { get; set; }
       public DateTime CreatedDate { get; set; }
    }
}
