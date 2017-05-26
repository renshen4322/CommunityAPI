using Community.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communiry.Entity.Comment
{
    public class TargetTypeEntity:BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
