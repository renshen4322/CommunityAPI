using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Community.OAuth.Exception
{
    [Serializable]
    public class OAuthTokenValidationException : System.Exception
    {
        public OAuthTokenValidationException() : base() { }
        public OAuthTokenValidationException(string message) : base(message) { }
        public OAuthTokenValidationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public OAuthTokenValidationException(string message, System.Exception inner) : base(message, inner) { }
    }
}
