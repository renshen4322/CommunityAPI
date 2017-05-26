
using Communiry.Entity.EnumType;
using Community.Core.Data;
using System;

namespace Communiry.Entity
{
   public class BaseUserEntity:BaseEntity
    {
       public Guid Id { get; set; }
       public int UserBaseId { get; set; }  
       public string NickName { get; set; }
       public string RealName { get; set; }
       public string Birthday { get; set; }
       public string Intro { get; set; }
       public GenderEnum Gender {
           get { 
               
               return (GenderEnum)Enum.Parse(typeof(GenderEnum), DbGender,true); }
           set { DbGender = value.ToString(); }  
       }
       public string DbGender { get; set; }
       public string Email { get; set; }
       public string Phone { get; set; }
       public string AreaCode { get; set; }
       public UserRoleEnum Role {
           get
           {

               return (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), DbRole, true);
           }
           set { DbRole = value.ToString(); }  
       }
       public string DbRole { get; set; }
       public DateTime CreatedDate { get; set; }
     
    }
}
