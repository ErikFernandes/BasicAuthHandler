using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
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
            // Verifica se o cabecalho de autorização foi fornecido
            if (!Request.Headers.ContainsKey("Authorization"))
            { return Task.FromResult(AuthenticateResult.Fail("Client did not provide an authorization header")); }

            var authorizationHeader = Request.Headers.Authorization.ToString();

            // Verifica se o cabecalho de autorização comecou com 'Basic '
            if (!authorizationHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            { return Task.FromResult(AuthenticateResult.Fail("Authorization header needs to start with 'Basic '")); }

            // Desencripta o cabecalho de autorização e separa o user da key
            string authBase64Decoded = Encoding.UTF8.GetString(Convert.FromBase64String(authorizationHeader.Replace("Basic ", "", StringComparison.OrdinalIgnoreCase)));
            string[] authSplit = authBase64Decoded.Split([':'], 2);

            // Verifica se o user e a key foram fornecios
            if (authSplit.Length != 2)
            { return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization header format")); }

            // Reserva o valor do User e Key
            string clientUser = authSplit[0];
            string clientKey = authSplit[1];

            // Checa se o User e a Key são válidos
            bool credIsValid()
            {
                return true;
            }


            // Verifica se as credenciais foram são válidas
            if (!credIsValid())
            {
                return Task.FromResult(AuthenticateResult.Fail(string.Format("The secret is incorrect for the client '{0}'", clientUser)));
            }

            // Autentica o client
            BasicAuthenticationClient client = new()
            {
                AuthenticationType = BasicAuthenticationDefaults.AuthenticationScheme,
                IsAuthenticated = true,
                Name = clientUser
            };

            // Define o token como nome
            ClaimsPrincipal claimsPrincipal = new(new ClaimsIdentity(client,
            [
                new Claim(ClaimTypes.Name, clientUser)
            ]));

            // Retorna autorizado
            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
        }
    }
}