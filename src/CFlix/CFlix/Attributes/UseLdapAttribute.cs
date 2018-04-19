using CFlix.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.Attributes
{
    public class UseLdapAttribute : TypeFilterAttribute
    {
        public UseLdapAttribute(bool use) : base(typeof(UseLdapAttributeImpl))
        {
            Arguments = new object[] { use };
        }

        private class UseLdapAttributeImpl : IAsyncResourceFilter
        {
            private readonly CFlixConfiguration _configuration;
            private readonly bool _use;

            public UseLdapAttributeImpl(IOptions<CFlixConfiguration> configuration, bool use)
            {
                _configuration = configuration.Value;
                _use = use;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                if (_configuration.UseLdap == _use)
                {
                    await next();
                }
                else
                {
                    context.Result = new NotFoundResult();
                }
            }
        }
    }
}
