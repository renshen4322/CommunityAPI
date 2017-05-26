using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Community.Common.Exception
{
    public class CommandException : System.Exception
    {
        public CommandException() : base() { }
        public CommandException(string message) : base(message) { }
        public CommandException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public CommandException(string message, System.Exception inner) : base(message, inner) { }
    }
}
