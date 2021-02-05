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
    /// کنترلر ویژگی های محصولات
    /// </summary>
    [ApiVersion("1")]
    public class ProductFeatureController : BaseApiController<ProductFeature>
    {
        private readonly IRepository<ProductFeature> repository;
        public ProductFeatureController(IRepository<ProductFeature> repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
