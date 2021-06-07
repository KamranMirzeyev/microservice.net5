using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Validation;
using IndentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace IndentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existuser = await _userManager.FindByEmailAsync(context.UserName);

            if (existuser==null)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors",new List<string>{"Password or email wrong!"});
                context.Result.CustomResponse = errors;
                return;
            }

            var password = await _userManager.CheckPasswordAsync(existuser,context.Password);

            if (!password)
            {

                var errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "Password or email wrong!" });
                context.Result.CustomResponse = errors;
                return;
            }

            context.Result =
                new GrantValidationResult(existuser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}
