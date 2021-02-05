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
    /// کنترلر محتوا
    /// </summary>
    [ApiVersion("1")]
    public class ContentController : BaseApiController<Content>
    {
        private readonly IRepository<Content> repository;
        public ContentController(IRepository<Content> repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}