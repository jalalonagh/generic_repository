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
    /// کنترلر محصولات
    /// </summary>
    [ApiVersion("1")]
    public class FeatureController : BaseApiController<Feature>
    {
        private readonly IRepository<Feature> repository;
        public FeatureController(IRepository<Feature> repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
