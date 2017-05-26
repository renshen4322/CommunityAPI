using Communiry.Entity.EnumType;
using Community.Core.Data;
using System;

namespace Communiry.Entity
{
   public class UserTagEntity : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public UserRoleEnum UserRole
        {
            get
            {

                return (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), DbUserRole, true);
            }
            set { DbUserRole = value.ToString(); }
        }
        public string DbUserRole { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
