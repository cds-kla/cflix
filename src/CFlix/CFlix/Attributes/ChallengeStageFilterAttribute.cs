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
    public class ChallengeStageFilterAttribute : TypeFilterAttribute
    {
        public ChallengeStageFilterAttribute(int minStage) : base(typeof(ChallengeStageFilterImpl))
        {
            Arguments = new object[] { minStage };
        }

        private class ChallengeStageFilterImpl : IResourceFilter
        {
            private readonly CFlixConfiguration _options;
            private readonly int _minStage;

            public ChallengeStageFilterImpl(IOptions<CFlixConfiguration> options,
                int minStage)
            {
                _options = options.Value;
                _minStage = minStage;
            }

            public void OnResourceExecuting(ResourceExecutingContext context)
            {
                if (_options.Stage < _minStage)
                {
                    context.Result = new NotFoundResult();
                }

            }
            
            public void OnResourceExecuted(ResourceExecutedContext context)
            {
            }
        }
    }
}
