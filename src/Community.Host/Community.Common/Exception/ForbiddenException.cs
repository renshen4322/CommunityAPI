using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Community.Common.Exception
{
    public class ForbiddenException : System.Exception
    {
        public ForbiddenException() : base() { }
        public ForbiddenException(string message) : base(message) { }
        public ForbiddenException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public ForbiddenException(string message, System.Exception inner) : base(message, inner) { }
    }
}
