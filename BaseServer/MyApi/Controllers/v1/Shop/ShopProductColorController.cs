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
    /// کنترلر محصولات فروشگاه
    /// </summary>
    [ApiVersion("1")]
    public class ShopProductColorController : BaseApiController<ShopProductColor>
    {
        private readonly IRepository<ShopProductColor> repository;
        public ShopProductColorController(IRepository<ShopProductColor> repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
