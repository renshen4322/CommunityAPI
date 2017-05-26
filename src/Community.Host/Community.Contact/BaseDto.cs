using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact
{
    /// <summary>
    /// 不需要带验证的请求基类
    /// </summary>
    public abstract class BaseRequestDto
    {
        //public string DeviceVersion { get; set; }

    }

    /// <summary>
    /// 不需要带验证的响应基类
    /// </summary>
    public abstract class BaseResponseDto
    {

    }

    /// <summary>
    /// 需要带验证的请求基类
    /// </summary>
    public  class CommandRequestDto : BaseRequestDto
    {
      //  public string UserId { get; set; }
    }

    /// <summary>
    /// 需要带验证的响应基类
    /// </summary>
    public abstract class CommandResponseDto : BaseResponseDto
    {
      
    }
    /// <summary>
    /// 批量查询请求基类
    /// </summary>
    public abstract class QueryRequestDto : BaseRequestDto
    {

        /// <summary>
        /// indicate sorting order, e.g. ?sort=-Name, -Name means order by Name descendingly
        /// </summary>
       // public string Sort { get; set; }

        /// <summary>
        /// 起始条数 ?start=50&length=100
        /// </summary>
        [Required]
        public int start { get; set; }

        /// <summary>
        ///获取记录数量 ?start=50&length=100
        /// </summary>
        [Required]
        public int length { get; set; }

        /// <summary>
        /// 默认从第0条开始 取20条
        /// </summary>
        protected QueryRequestDto()
        {
            start =0;
            length =20;
        }
    }

    /// <summary>
    /// 批量查询响应基类
    /// </summary>
    public abstract class QueryResponseDto : BaseResponseDto
    {
        public QueryResponseDto()
        {
            total = 0;
        }

        public long? total { get; set; }
      //  public MetaData _metaData { get; set; }
    }
    public class MetaData
    {
        public long? total { get; set; }
        public int? limit { get; set; }
        public long? offset { get; set; }
    }
}
