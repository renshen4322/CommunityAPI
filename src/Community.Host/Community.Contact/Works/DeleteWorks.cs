using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Works
{
    /// <summary>
    /// 删除作品
    /// </summary>
    public class DeleteWorks : CommandRequestDto
    {
        /// <summary>
        /// 作品id
        /// </summary>
        [Required]
        public Guid works_id { get; set; }
    }
    public class DeleteWorksResponse : CommandResponseDto
    {
    }
}
