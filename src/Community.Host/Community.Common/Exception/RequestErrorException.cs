using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Community.Common.Exception
{
    public class RequestErrorException : System.Exception
    {
        public RequestErrorException() : base() { }
        public RequestErrorException(string message) : base(message) { }
        public RequestErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public RequestErrorException(string message, System.Exception inner) : base(message, inner) { }
    }
}
