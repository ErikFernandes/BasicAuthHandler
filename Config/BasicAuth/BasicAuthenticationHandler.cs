using BasicAuthHandler.Global;
using BasicAuthHandler.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace RoundTheCode.BasicAuthentication.Shared.Authentication.Basic.Handlers
{
    public class BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder) :
        AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
    {
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Check if client did not provide an IP address
            if (Request.HttpContext.Connection.RemoteIpAddress == null)
            { return Task.FromResult(AuthenticateResult.Fail("Client did not provide an IP address")); }

            // Check if authorization header was provided
            if (!Request.Headers.ContainsKey("Authorization"))
            { return Task.FromResult(AuthenticateResult.Fail("Client did not provide an authorization header")); }


            var authorizationHeader = Request.Headers.Authorization.ToString();

            // Check if authorization header starts with 'Basic '
            if (!authorizationHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            { return Task.FromResult(AuthenticateResult.Fail("Authorization header needs to start with 'Basic '")); }

            // Decode authorization header and separate user from key
            string authBase64Decoded = Encoding.UTF8.GetString(Convert.FromBase64String(authorizationHeader.Replace("Basic ", "", StringComparison.OrdinalIgnoreCase)));
            string[] authSplit = authBase64Decoded.Split([':'], 2);

            // Check if user and key were provided
            if (authSplit.Length != 2)
            { return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization header format")); }

            // Store the value of User and Key
            string clientUser = authSplit[0];
            string clientKey = authSplit[1];

            // Check if User and Key are valid
            bool credIsValid()
            {
                AcceptUserKeysModel model = new()
                {
                    Username = clientUser,
                    Key = clientKey
                };

                return GlobalVariables.AcceptUserKeys.Any(x => x.Username == clientUser && x.Key == clientKey);
            }

            // Check if credentials are valid
            if (!credIsValid())
            {
                return Task.FromResult(AuthenticateResult.Fail(string.Format("The secret is incorrect for the client '{0}'", clientUser)));
            }

            // Authenticate the client
            BasicAuthenticationClient client = new()
            {
                AuthenticationType = BasicAuthenticationDefaults.AuthenticationScheme,
                IsAuthenticated = true,
                Name = clientUser
            };

            // Set the token as name
            ClaimsPrincipal claimsPrincipal = new(new ClaimsIdentity(client,
            [
                new Claim(ClaimTypes.Name, clientUser)
            ]));

            #region Add client to list of clients accepted

            IPAddress ipClient = Request.HttpContext.Connection.RemoteIpAddress;
            if (GlobalVariables.ClientsAccept.TryGetValue(ipClient, out ClientAcceptModel? value))
            {
                value.HitsAccept++;
                value.LastAccessDate = DateTime.Now;
                value.Username = clientUser;
                value.Key = clientKey;
            }
            else 
            {
                GlobalVariables.ClientsAccept.TryAdd(ipClient, new ClientAcceptModel
                {
                    IpAddress = ipClient,
                    HitsAccept = 1,
                    Username = clientUser,
                    Key = clientKey
                });
            }

            #endregion

            // Return authorized
            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
        }
    }
}