using CFlix.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.Controllers
{
    [Authorize]
    [ChallengeStageFilter(2)]
    public class SubscriptionController : Controller
    {
        [AuthorizationHeaderFilter("1897cfda75a75589cf3d978b5ca3b8160b4b7491b620e186d978fa947b98385b")]
        [Route("[controller]/subscribe_test")]
        public IActionResult IndexPreview()
        {
            return View();
        }
    }
}
