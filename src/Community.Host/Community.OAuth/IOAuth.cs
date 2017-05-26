using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Community.OAuth
{
  public interface IOAuth
    {
      OAuthToken AcquireToken(string authorization);
    }
  public class OauthUser {
      public int userId { get; set; }
      public string email { get; set; }
      public string userName { get; set; }
      public string nick { get; set; }
      public string realName { get; set; }
      public string avatar { get; set; }
      public string mobile { get; set; }
      public string address { get; set; }
      public string appearanceId { get; set; }
      public string gender { get; set; }
  }
  public class OAuthToken : IIdentity
  {

      public Guid? UserId { get; set; }
      public int OauthUserId { get; set; }
      public string userName { get; set; }
      public string mobile { get; set; }
      public string email { get; set; }
      public string Token { get; set; }
      public string ResponseCode { get; set; }     
      public string Scope { get; set; }
      public string ExpirationDateUTC { get; set; }
      public Error Error { get; set; }
      public string AuthenticationType { get; set; }


      public bool IsAuthenticated { get; set; }

      public string Name { get; private set; }
  }
  public class UserPrincipal: IPrincipal
  {
      public UserPrincipal(OAuthToken identity)
      {
          Identity = identity;
      }

      /// <summary>
      /// 
      /// </summary>
      public OAuthToken Identity { get; private set; }

      IIdentity IPrincipal.Identity
      {
          get { return Identity; }
      }


      bool IPrincipal.IsInRole(string role)
      {
          throw new NotImplementedException();
      }
  }

    public class Error{
        public string Msg{get;set;}
        
    }
}
