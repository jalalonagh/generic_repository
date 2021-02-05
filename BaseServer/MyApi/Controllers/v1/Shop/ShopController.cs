using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.BS.Contracts;

namespace Moradi.Controllers.v1.Shop
{
    /// <summary>
    /// کنترلر قروشگاه
    /// </summary>
    [ApiVersion("1")]
    public class ShopController : BaseApiController<Entities.Shop.Shop>
    {
        private readonly IRepository<Entities.Shop.Shop> repository;
        public ShopController(IRepository<Entities.Shop.Shop> repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
