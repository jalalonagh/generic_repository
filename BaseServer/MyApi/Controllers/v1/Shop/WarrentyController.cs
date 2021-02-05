using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using Entities.Shop;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.BS.Contracts;

namespace Moradi.Controllers.v1.Shop
{
    /// <summary>
    /// کنترلر لیک کردن
    /// </summary>
    [ApiVersion("1")]
    public class WarrentyController : BaseApiController<Warrenty>
    {
        private readonly IRepository<Warrenty> repository;
        public WarrentyController(IRepository<Warrenty> repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
