using Community.Common;
using Community.Core.Data;
using Community.EntityFramework;
using Community.OAuth.Exception;
using NLog;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using Community.Utils.Common;

namespace Community.OAuth
{
    public class OAuth : IOAuth
    {
        const string BEARER = "Bearer";
        const string MISSING_AUTH = "Bearer realm=\"Api\",error=\"invalid_token\", error_description=\"The access token is missing\"";
        const string BAD_TOKEN = "Bearer realm=\"Api\", error=\"invalid_token\", error_description=\"The access token is invalid\"";
        const string EXPIRED_TOKEN = "Bearer realm=\"Api\", error=\"invalid_token\", error_description=\"The access token is expired\"";
/*
        const string Authorization = "Authorization";
*/
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public OAuthToken AcquireToken(string authorization)
        {

            var tokenString = ExtractTokenString(authorization);

            OAuthToken cachedOAuthToken;
            if (string.IsNullOrEmpty(tokenString))
            {
                // _logger.Debug("Ending request with code {0}, message {1}...", 401, MISSING_AUTH);
                cachedOAuthToken = new OAuthToken {Error = new Error() {Msg = MISSING_AUTH}};
            }
            else
            {
                cachedOAuthToken = CacheHelper.Get<OAuthToken>(tokenString);


                if (cachedOAuthToken == null)
                {
                    var result = Validate(tokenString);
                    if (string.IsNullOrEmpty(result))
                    {
                        var errOt = new OAuthToken {Error = new Error() {Msg = BAD_TOKEN}};
                        return errOt;
                    }
                    var oauthUser = JsonDeserialize<OauthUser>(result);
                    cachedOAuthToken = new OAuthToken
                    {
                        OauthUserId = oauthUser.userId,
                        email = oauthUser.email,
                        mobile = oauthUser.mobile,
                        userName = oauthUser.userName,
                        Token = tokenString,
                        ExpirationDateUTC = DateTime.UtcNow.AddMinutes(30).ToString(CultureInfo.InvariantCulture),
                        UserId = GetUserId(oauthUser.userId)
                    };

                    #region error code
                    //if (oAuthToken.ResponseCode == "0")
                   // {
                        // _logger.Debug("Validation success, added token object to cache");
                       
                   // }
                    //else if (oAuthToken.ResponseCode == "100013")
                    //{
                    //   // _logger.Debug("Ending request with code {0}, message {1}...", 401, BAD_TOKEN);
                    //    res.TerminateWith(401, BAD_TOKEN);
                    //}
                    //else if (oAuthToken.ResponseCode == "100010")
                    //{
                    //    _logger.Debug("Ending request with code {0}, message {1}...", 401, EXPIRED_TOKEN);
                    //    res.TerminateWith(401, EXPIRED_TOKEN);
                    //}
                    #endregion
                    #region 获取系统用户信息
                        
                    #endregion
                    CacheHelper.Insert<OAuthToken>(tokenString, cachedOAuthToken, 30);
                   
                }
                else
                {
                    //  _logger.Debug("Hit cached token");
                    var expireUtcDate = DateTime.Parse(cachedOAuthToken.ExpirationDateUTC);
                    if (expireUtcDate <= DateTime.UtcNow.AddSeconds(15))
                    {
                        // _logger.Debug("Cached Token expired, ending request with code {0}, message {1}...", 401, EXPIRED_TOKEN);
                        CacheHelper.Remove(tokenString);
                        cachedOAuthToken.Error = new Error() { Msg = EXPIRED_TOKEN };
                    }
                    else {
                        if (cachedOAuthToken.UserId == null)
                        {
                            cachedOAuthToken.UserId = GetUserId(cachedOAuthToken.OauthUserId);
                        }
                    }
                  
                }
            }
            // return cachedOAuthToken;
            return cachedOAuthToken;
        }
        /// <summary>
        /// 获取用户对应id
        /// </summary>
        /// <param name="baseUserId"></param>
        /// <returns></returns>
        private Guid? GetUserId(int baseUserId) {
            try
            {
                IDapperRepository dbRepository = new MysqlDapperRepository(GlobalAppSettings.MysqlConnection);
                return dbRepository.QuerySingleOrDefault<Guid?>("select id from community_base_user where user_base_id=" + baseUserId);
    
            }
            catch (System.Exception ex)
            {
                _logger.Error("get userid by OauthId:{0}, failed because {1}", baseUserId, ex.Message);             
                return null;
            }
              }

        private string Validate(string tokenString)
        {
          
            #region 正式逻辑
            var url = GlobalAppSettings.OAuthServiceUrl;
            // _logger.Debug("Validating access token string {0} against {1}", tokenString, url);
            try
            {
                var req = WebRequest.Create(url) as HttpWebRequest;
                if (req != null)
                {
                    req.Method = "GET";
                    req.Headers.Add("Authorization", tokenString);
                    // req.ContentType = "application/x-www-form-urlencoded";
                    //using (var rs = req.GetRequestStream())
                    //{
                    //    var formDataString = "access_token=" + tokenString;
                    //    var formData = Encoding.UTF8.GetBytes(formDataString);
                    //    rs.Write(formData, 0, formData.Length);
                    //}

                    var res = req.GetResponse();
                    string result;
                    using (var receivedStream = res.GetResponseStream())
                    {
                        if (receivedStream == null) return null;
                        var streamReader = new StreamReader(receivedStream);
                        result = streamReader.ReadToEnd();
                    }
                    return result;
                }
            }
            catch (WebException ex)
            {
               
                _logger.Error("Validation on access token string {0} against {1}, failed because {2}", tokenString, url, ex.Message);
                throw new OAuthTokenValidationException(ex.Message, ex.InnerException);
            }
            #endregion

            return null;
        }

        private static string ExtractTokenString(string authorization)
        {
            string tokenString;
            var authz = authorization;
            if (!string.IsNullOrEmpty(authorization) && authz.Contains(BEARER))
            {
               // _logger.Debug("Reading access token from Header base on Bearer scheme...");
                tokenString = authz.TrimStart(BEARER.ToCharArray()).Trim();
            }
            else
            {
               //_logger.Debug("Reading access token from request parameter...");
                tokenString = authorization;
            }
            return tokenString;
        }

        private static T JsonDeserialize<T>(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt))
            {
                return default(T);
            }

            var ser = new DataContractJsonSerializer(typeof(T));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(txt)))
            {
                return (T)ser.ReadObject(ms);
            }
        }
    }
}
