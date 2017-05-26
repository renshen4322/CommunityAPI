using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.OAuth
{
    public static class ExtendServiceStack
    {
        public static void TerminateWith(this IOwinResponse res, int errorcode, string message)
        {
            res.StatusCode = errorcode;
            res.Headers.Append("WWW-Authenticate", message);
            
        }
    }
}
