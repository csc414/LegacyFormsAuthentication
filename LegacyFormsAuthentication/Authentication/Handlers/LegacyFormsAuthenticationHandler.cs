using AspNetCore.LegacyAuthCookieCompat;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace YSWL.Shop.Authentication.Handlers
{
    public class LegacyFormsAuthenticationHandler : AuthenticationHandler<LegacyFormsAuthenticationOptions>
    {
        private static object lockObj = new object();

        private static LegacyFormsAuthenticationTicketEncryptor _encryptor;

        public LegacyFormsAuthenticationHandler(IOptionsMonitor<LegacyFormsAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var cookie = Request.Cookies[Options.CookieName];
            if (string.IsNullOrEmpty(cookie))
                return Task.FromResult(AuthenticateResult.NoResult());

            if (_encryptor == null)
            {
                lock(lockObj)
                {
                    if (_encryptor == null)
                        _encryptor = new LegacyFormsAuthenticationTicketEncryptor(Options.DecryptionKey, Options.ValidationKey, Options.ShaVersion, Options.CompatibilityMode);
                }
            }

            var formsTicket = _encryptor.DecryptCookie(cookie);

            if (formsTicket.Expired)
                return Task.FromResult(AuthenticateResult.Fail("Ticket expired"));

            var identity = new ClaimsIdentity(Scheme.Name);
            identity.AddClaim(new Claim(ClaimTypes.Name, formsTicket.Name));
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, new AuthenticationProperties
            {
                IssuedUtc = formsTicket.IssueDate,
                ExpiresUtc = formsTicket.Expiration,
                IsPersistent = formsTicket.IsPersistent
            }, ClaimsIssuer);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            var redirectUri = properties.RedirectUri;
            if (string.IsNullOrEmpty(redirectUri))
                redirectUri = new StringBuilder()
                .Append(Request.Scheme)
                .Append("://")
                .Append(Request.Host)
                .Append(OriginalPathBase)
                .Append(OriginalPath)
                .Append(Request.QueryString)
                .ToString();

            var loginUri = Options.LoginPath + QueryString.Create(Options.ReturnUrlParameter, redirectUri);
            if (IsAjaxRequest(Context.Request))
            {
                Context.Response.Headers["Location"] = loginUri;
                Context.Response.StatusCode = 401;
            }
            else
            {
                Context.Response.Redirect(loginUri);
            }
            return Task.CompletedTask;
        }

        private static bool IsAjaxRequest(HttpRequest request)
        {
            return string.Equals(request.Query["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal) ||
                string.Equals(request.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal);
        }
    }
}
