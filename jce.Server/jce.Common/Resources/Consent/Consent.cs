

using IdentityServer4.Models;

namespace jce.Common.Resources.Consent
{
    public class Consent
    {
        public Client Client { get; set; }

        public IdentityServer4.Models.Resources Resource { get; set; }

        public AuthorizationRequest Request { get; set; }

        public Consent(Client client, IdentityServer4.Models.Resources resource, AuthorizationRequest request)
        {
            Client = client;
            Resource = resource;
            Request = request;
        }
    }
}
