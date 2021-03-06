using System;
using Microsoft.AspNetCore.Http;

namespace Framework.Common
{
    public class CurrentUser:ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserIp()
        {
            return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }   
        public string GetUserId()
        {
            
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();

            return userId;
        }
        public string GetUserIdFromHeader()
        {
            var checkHeaderIsExist=_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("userId", out var traceValue);

            if (checkHeaderIsExist is true)
            {
                return traceValue;
            }

            return null;
        }
        public void SetHttpOnlyUserCookie(string key, string value,DateTimeOffset date,string webSite)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Lax,Expires = date,Domain = webSite,Secure = false});
        }

        public void CleanSecurityCookie(string key)
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(key))
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
        }
        
    }
}