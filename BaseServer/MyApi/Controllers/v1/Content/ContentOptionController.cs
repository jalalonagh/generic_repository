using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using Entities.Base;
using Entities.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.BS.Contracts;

namespace Moradi.Controllers.v1.Base
{
    /// <summary>
    /// کنترلر کامنت های محتوا
    /// </summary>
    [ApiVersion("1")]
    public class ContentOptionController : BaseApiController<ContentOption>
    {
        private readonly IRepository<ContentOption> repository;
        public ContentOptionController(IRepository<ContentOption> repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}