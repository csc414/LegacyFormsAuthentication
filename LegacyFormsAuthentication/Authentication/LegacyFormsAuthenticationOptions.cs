using AspNetCore.LegacyAuthCookieCompat;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YSWL.Shop.Authentication
{
    public class LegacyFormsAuthenticationOptions : AuthenticationSchemeOptions
    {
        /// <summary>
        /// CookieName
        /// </summary>
        public string CookieName { get; set; } = ".ASPXAUTH";

        /// <summary>
        /// LoginPath
        /// </summary>
        public string LoginPath { get; set; }

        /// <summary>
        /// ReturnUrlParameter
        /// </summary>
        public string ReturnUrlParameter { get; set; } = "ReturnUrl";

        /// <summary>
        /// DecryptionKey
        /// </summary>
        public string DecryptionKey { get; set; }

        /// <summary>
        /// ValidationKey
        /// </summary>
        public string ValidationKey { get; set; }

        /// <summary>
        /// Encrypt Mode
        /// </summary>
        public ShaVersion ShaVersion { get; set; } = ShaVersion.Sha1;

        /// <summary>
        /// Framework Version
        /// </summary>
        public CompatibilityMode CompatibilityMode { get; set; } = CompatibilityMode.Framework20SP2;
    }
}
