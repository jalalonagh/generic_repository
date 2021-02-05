﻿using Microsoft.AspNetCore.Mvc;
using WebFramework.Filters;

namespace WebFramework.Api
{
    [ApiController]
    [ApiResultFilter]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseController : ControllerBase
    {
        public bool UserIsAutheticated => HttpContext.User.Identity.IsAuthenticated;
    }
}
