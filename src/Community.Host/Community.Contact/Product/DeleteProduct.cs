using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Product
{
    /// <summary>
    /// 删除产品
    /// </summary>
   public class DeleteProduct:CommandRequestDto
    {
        /// <summary>
        /// 产品id
        /// </summary>
        [Required]
        public Guid product_id { get; set; }
    }

    public class DeleteProductResponse : CommandResponseDto {

    }
}
