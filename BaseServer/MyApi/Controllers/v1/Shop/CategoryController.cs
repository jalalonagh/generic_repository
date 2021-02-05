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
    /// کنترلر دسته بندی
    /// </summary>
    [ApiVersion("1")]
    public class CategoryController : BaseApiController<Category>
    {
        private readonly IRepository<Category> repository;
        public CategoryController(IRepository<Category> repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
