using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using Entities.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Moradi.Controllers.v1.Base
{
    [ApiVersion("1")]
    public class MenuController : BaseApiController<Menu>
    {
        private readonly IRepository<Menu> repository;
        public MenuController(IRepository<Menu> repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
