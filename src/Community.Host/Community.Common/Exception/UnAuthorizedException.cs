using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Community.Common.Exception
{
   public class UnAuthorizedException : System.Exception
    {
        public UnAuthorizedException() : base() { }
        public UnAuthorizedException(string message) : base(message) { }
        public UnAuthorizedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public UnAuthorizedException(string message, System.Exception inner) : base(message, inner) { }
    }
}
