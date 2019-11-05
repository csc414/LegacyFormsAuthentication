using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSWL.Shop.Authentication;
using YSWL.Shop.Authentication.Handlers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LegacyFormsAuthenticationExtension
    {
        public const string DefaultAuthenticationScheme = "LegacyForms";

        public static AuthenticationBuilder AddLegacyFormsAuthentication(this AuthenticationBuilder builder)
        => builder.AddLegacyFormsAuthentication(DefaultAuthenticationScheme);

        public static AuthenticationBuilder AddLegacyFormsAuthentication(this AuthenticationBuilder builder, string authenticationScheme)
            => builder.AddLegacyFormsAuthentication(authenticationScheme, configureOptions: null);

        public static AuthenticationBuilder AddLegacyFormsAuthentication(this AuthenticationBuilder builder, Action<LegacyFormsAuthenticationOptions> configureOptions)
            => builder.AddLegacyFormsAuthentication(DefaultAuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddLegacyFormsAuthentication(this AuthenticationBuilder builder, string authenticationScheme, Action<LegacyFormsAuthenticationOptions> configureOptions)
            => builder.AddLegacyFormsAuthentication(authenticationScheme, displayName: null, configureOptions: configureOptions);

        public static AuthenticationBuilder AddLegacyFormsAuthentication(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<LegacyFormsAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<LegacyFormsAuthenticationOptions, LegacyFormsAuthenticationHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}
