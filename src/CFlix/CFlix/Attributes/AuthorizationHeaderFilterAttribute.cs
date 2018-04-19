using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CFlix.Attributes
{
    public class AuthorizationHeaderFilterAttribute : TypeFilterAttribute
    {
        public AuthorizationHeaderFilterAttribute(string value) : base(typeof(AuthorizationHeaderFilterImpl))
        {
            Arguments = new object[] { value };
        }

        private class AuthorizationHeaderFilterImpl : IResourceFilter
        {
            private readonly string _value;

            public AuthorizationHeaderFilterImpl(string value)
            {
                _value = value;
            }

            public void OnResourceExecuting(ResourceExecutingContext context)
            {
                if (!(context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationToken)
                      && GenerateHash(authorizationToken) == _value))
                {
                    context.Result = new ForbidResult();
                }
            }

            public void OnResourceExecuted(ResourceExecutedContext context)
            {
            }

            // j'ai rajouté la vérification par hash au cas où le code source serait trouvé par des petits malins :P
            private string GenerateHash(string clear)
            {
                return BitConverter.ToString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(clear)))
                    .Replace("-", string.Empty)
                    .ToLower();
            }
        }
    }
}
