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
    /// کنترلر پیام های کاربران
    /// </summary>
    [ApiVersion("1")]
    public class UserMessageController : BaseApiController<UserMessage>
    {
        private readonly IRepository<UserMessage> repository;
        public UserMessageController(IRepository<UserMessage> repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}