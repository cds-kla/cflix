using CFlix.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.Services
{
    public class AccountTypeRequirement : IAuthorizationRequirement
    {
        public AccountTypeRequirement(AccountType accountType)
        {
            AccountType = accountType;
        }

        public AccountType AccountType { get; set; }
    }

    public class AccountTypeAuthorizationHandler : AuthorizationHandler<AccountTypeRequirement>
    {
        private readonly UserManager<CFlixUser> _userManager;

        public AccountTypeAuthorizationHandler(UserManager<CFlixUser> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AccountTypeRequirement requirement)
        {
            var user = await _userManager.GetUserAsync(context.User);

            if (user?.AccountType == requirement.AccountType)
            {
                context.Succeed(requirement);
            }
        }
    }
}
