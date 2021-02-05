using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using Entities.Base;
using Entities.Content;
using Entities.Shop;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moradi.Models;
using WebFramework.Api;

namespace Moradi.Controllers.v1
{
    [ApiVersion("1")]
    public class IntegrationController : BaseController
    {
        private readonly IRepository<Product> product;
        private readonly IRepository<ShopProduct> store;
        private readonly IRepository<Category> category;
        private readonly IRepository<Menu> menu;
        private readonly IRepository<Option> option;
        private readonly IRepository<Entities.Shop.Shop> shop;
        private readonly IRepository<Content> content;
        private readonly IRepository<Warrenty> warrenties;

        public IntegrationController(IRepository<Product> _product,
            IRepository<ShopProduct> _store,
            IRepository<Category> _category,
            IRepository<Menu> _menu,
            IRepository<Entities.Shop.Shop> _shop,
            IRepository<Option> _option,
            IRepository<Content> _content,
            IRepository<Warrenty> _warrenties)
        {
            product = _product;
            menu = _menu;
            store = _store;
            category = _category;
            option = _option;
            shop = _shop;
            content = _content;
            warrenties = _warrenties;
        }

        [HttpGet]
        public async Task<ApiResult<IntegratedModel>> Get()
        {
            var init = new IntegratedModel();

            init.categories = await category.GetAllAsync(new System.Threading.CancellationToken());
            init.contents = await content.GetAllAsync(new System.Threading.CancellationToken());
            init.menus = await menu.GetAllAsync(new System.Threading.CancellationToken());
            init.options = await option.GetAllAsync(new System.Threading.CancellationToken());
            init.products = await product.GetAllAsync(new System.Threading.CancellationToken());
            init.stores = await store.GetAllAsync(new System.Threading.CancellationToken());
            init.shops = await shop.GetAllAsync(new System.Threading.CancellationToken());
            init.warrenties = await warrenties.GetAllAsync(new System.Threading.CancellationToken());

            return init;
        }
    }
}
