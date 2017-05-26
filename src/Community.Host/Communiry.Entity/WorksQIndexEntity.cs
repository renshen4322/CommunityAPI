using Community.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communiry.Entity
{
    public class WorksQIndexEntity : BaseEntity
    {
      public int Id { get; set; }
      public Guid worksId { get; set; }
      public StateEnum StateName
      {
          get
          {
              return (StateEnum)Enum.Parse(typeof(StateEnum), DbStateName, true);
          }
          set
          {
              DbStateName = value.ToString();
          }

      }
      public string DbStateName { get; set; }
      public OptionTypeEnum Type
      {
          get
          {
              return (OptionTypeEnum)Enum.Parse(typeof(OptionTypeEnum), DbType, true);
          }
          set
          {
              DbType = value.ToString();
          }

      }
      public string DbType { get; set; }
      public DateTime CreatedDate { get; set; }
      public DateTime? UpdatedDate { get; set; }
    }

    public enum OptionTypeEnum
    {
        Created,
        Updated,
        Deleted
    }
    public enum StateEnum
    {

        Enqueued,
        Processing,
        Failed,
        Succeeded
    }
}
