using Microsoft.AspNetCore.Authorization;
using RoundTheCode.BasicAuthentication.Shared.Authentication.Basic;

namespace RoundTheCode.BasicAuthentication.Authentication.Basic.Attributes
{
    public class BasicAuthorizationAttribute : AuthorizeAttribute
    {
        public BasicAuthorizationAttribute()
        {
            AuthenticationSchemes = BasicAuthenticationDefaults.AuthenticationScheme;
        }
    }
}