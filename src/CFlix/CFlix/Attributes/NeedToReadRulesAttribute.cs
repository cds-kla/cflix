using CFlix.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using System;

namespace CFlix.Attributes
{
    public class NeedToReadRulesAttribute : TypeFilterAttribute
    {
        public NeedToReadRulesAttribute()
            : base(typeof(NeedToReadRulesAttributeImpl))
        {
        }

        private class NeedToReadRulesAttributeImpl : IAsyncResourceFilter
        {
            private readonly UserManager<CFlixUser> _userManager;

            public NeedToReadRulesAttributeImpl(UserManager<CFlixUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                var user = await _userManager.GetUserAsync(context.HttpContext.User);

                if (user?.HaveReadRules == true)
                {
                    await next();
                }
                else
                {
                    context.Result = new RedirectToActionResult("Index", "Rules", null);
                }
            }
        }
    }
}
