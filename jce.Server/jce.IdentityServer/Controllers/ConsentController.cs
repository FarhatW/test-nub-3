using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using jce.Common.Resources.Consent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Consent = jce.Common.Resources.Consent.Consent;

namespace jce.IdentityServer.Controllers
{
 
    [Route("api/consent")]
    [SecurityHeaders]
    public class ConsentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IClientStore _clientStore;
        private readonly IResourceStore _resourceStore;
        private readonly IIdentityServerInteractionService _interaction;

        public ConsentController(IClientStore clientStore, IResourceStore resourceStore, IIdentityServerInteractionService interaction, IMapper mapper)
        {
            _clientStore = clientStore;
            _resourceStore = resourceStore;
            _interaction = interaction;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GetConsentResource([FromBody] ConsentQuery returnUrl)
        {
            var request = await _interaction.GetAuthorizationContextAsync(returnUrl.ReturnUrl);
            if (request != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(request.ClientId);
                if (client != null)
                {
                    var resources = await _resourceStore.FindEnabledResourcesByScopeAsync(request.ScopesRequested);
                    if (resources != null && (resources.IdentityResources.Any() || resources.ApiResources.Any()))
                    {
                        var consent =  new Consent(client,resources,request);
                       
                        return Ok(_mapper.Map<Consent, ConsentResource>(consent));
                    }
                    else
                    {
                        throw new Exception("No scopes matching: {0}", new Exception(request.ScopesRequested.Aggregate((x, y) => x + ", " + y)));
                    }
                }
                else
                {
                    throw new Exception("Invalid client id: {0}", new Exception(request.ClientId));

                }
            }
            else
            {
                throw new Exception("No consent request matching request: {0} login");
            }
        
        }

        [AllowAnonymous]

        [HttpPost("process")]
        public async Task<IActionResult> ConsentProcess([FromBody] ConsentInputResource model)
        {
            var result = await ProcessConsent(model);
            if (!result.IsRedirect) return BadRequest(result.HasValidationError ? result.ValidationError : "blalala");

            return Ok(result);

        }

        public async Task<ProcessConsentResult> ProcessConsent(ConsentInputResource model)
        {
            var result = new ProcessConsentResult();

            ConsentResponse grantedConsent = null;

            // user clicked 'no' - send back the standard 'access_denied' response
            if (model.Button == "no")
            {
                grantedConsent = ConsentResponse.Denied;
            }
            // user clicked 'yes' - validate the data
            else if (model.Button == "yes")
            {
                // if the user consented to some scope, build the response model
                if (model.ScopesConsented != null && model.ScopesConsented.Any())
                {
                    var scopes = model.ScopesConsented;
                    if (ConsentOptions.EnableOfflineAccess == false)
                    {
                        scopes = scopes.Where(x => x != IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess);
                    }

                    grantedConsent = new ConsentResponse
                    {
                        RememberConsent = model.RememberConsent,
                        ScopesConsented = scopes.ToArray()
                    };
                }
                else
                {
                    result.ValidationError = ConsentOptions.MustChooseOneErrorMessage;
                }
            }
            else
            {
                result.ValidationError = ConsentOptions.InvalidSelectionErrorMessage;
            }

            if (grantedConsent != null)
            {
                // validate return url is still valid
                var request = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
                if (request == null) return result;

                try
                {
                    await _interaction.GrantConsentAsync(request, grantedConsent);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                // communicate outcome of consent back to identityserver
      
                // indicate that's it ok to redirect back to authorization endpoint
                result.RedirectUri = model.ReturnUrl;
            }
            else
            {
                // we need to redisplay the consent UI
                return null;
               
            }

            return result;
        }


    }
}