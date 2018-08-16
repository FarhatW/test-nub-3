using System.Collections.Generic;

namespace jce.Common.Resources.Consent
{
    public class ConsentResource
    {
        public string ClientName { get; set; }
        public string ClientUrl { get; set; }
        public string ClientLogoUrl { get; set; }
        public bool AllowRememberConsent { get; set; }

        public IEnumerable<ScopeResource> IdentityScopes { get; set; }
        public IEnumerable<ScopeResource> ResourceScopes { get; set; }
    }
}
