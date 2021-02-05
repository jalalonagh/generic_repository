using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using Entities.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.BS.Contracts;

namespace Moradi.Controllers.v1.Base
{
    /// <summary>
    /// کنترلر کامنت های کاربران
    /// </summary>
    [ApiVersion("1")]
    public class UserCommentController : BaseApiController<UserComment>
    {
        private readonly IRepository<UserComment> repository;
        public UserCommentController(IRepository<UserComment> repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}