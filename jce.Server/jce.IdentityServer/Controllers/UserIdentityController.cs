using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer4.Services;
using jce.BusinessLayer.IManagers;
using jce.Common.Entites.IdentityServerDbContext;
using jce.Common.Resources.user;
using jce.Common.Resources.userIdentity;
using jce.Common.Resources.UserProfile;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace jce.IdentityServer.Controllers
{

    [Produces("application/json")]
    [Route("api/users")]

    public class UserIdentityController : Controller
    {
        private readonly IUserIdentiyManager _userIdentiyManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
 
        public UserIdentityController(IIdentityServerInteractionService interaction, SignInManager<User> signInManager, IUserIdentiyManager userIdentiyManager)
        {
            _interaction = interaction;
            _signInManager = signInManager;
            _userIdentiyManager = userIdentiyManager;
        }

        [HttpGet]
    
        public async Task<IActionResult> GetUser(UserQueryResource filterResource)
        {
            var queryResult = await _userIdentiyManager.GetAll(filterResource);

            if (queryResult == null)
                return NotFound();

            return Ok(queryResult);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetUserById(int id)
        {
            var userProfileResource = await _userIdentiyManager.GetItemById(id);

            if (userProfileResource == null)
                return NotFound();

            return Ok(userProfileResource);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]SaveUserResource saveUserResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _userIdentiyManager.Add(saveUserResource));

        }
        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateUserResource saveUserResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _userIdentiyManager.Update(id, saveUserResource));

        }
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody]AuthResource employeeResource)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var baseUri = new Uri(employeeResource.ReturnUrl);

            var context = await _interaction.GetAuthorizationContextAsync(baseUri.PathAndQuery);

            if (context == null) return Unauthorized();

            var result = await _signInManager.PasswordSignInAsync(employeeResource.Email, employeeResource.Password, employeeResource.RememberMe, lockoutOnFailure: false);
            if (!result.Succeeded) return BadRequest("cannot login");
            var res = RedirectToLocal(baseUri.PathAndQuery);
            return Ok(res);
        }

        private ProcessLoginResult RedirectToLocal(string returnUrl)
        {
            var result = new ProcessLoginResult();

            if (Url.IsLocalUrl(returnUrl))
            {
                result.RedirectUri = returnUrl;

            }
            return result;
        }

        [HttpGet("logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout(string logoutId)
        {
            // build a model so the logout page knows what to display
            var vm = await _userIdentiyManager.LogoutAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
            {
                // if the request for logout was properly authenticated from IdentityServer, then
                // we don't need to show the prompt and can just log the user out directly.
                return await Logout(vm);
            }

            return Ok(vm);
        }

        [HttpPost("logout")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogOutInputResource model)
        {
            var vm = await _userIdentiyManager.LoggedOutAsync(model.LogoutId);

            await _signInManager.SignOutAsync();
            

            // check if we need to trigger sign-out at an upstream identity provider
            if (vm.TriggerExternalSignout)
            {
                // build a return URL so the upstream provider will redirect back
                // to us after the user has logged out. this allows us to then
                // complete our single sign-out processing.
                string url = Url.Action("Logout", new { logoutId = vm.LogoutId });

                // this triggers a redirect to the external provider for sign-out
                // hack: try/catch to handle social providers that throw
                return SignOut(new AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme);
            }

            return Ok(vm);
            //return View("LoggedOut", vm);
        }

    }
}