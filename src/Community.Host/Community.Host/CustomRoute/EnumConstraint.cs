using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Routing;
using System.Web.Routing;

namespace Community.Host.CustomRoute
{
    /// <summary>
    /// 枚举约束
    /// </summary>
    public class EnumConstraint : IHttpRouteConstraint 
    {
        private readonly string[] _validOptions;
        
        public EnumConstraint(string options)
        {
            _validOptions = options.Split('|');
        }
        

        public bool Match(System.Net.Http.HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object value;
            if (values.TryGetValue(parameterName, out value) && value != null)
            {
                return _validOptions.Contains(value.ToString(), StringComparer.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}