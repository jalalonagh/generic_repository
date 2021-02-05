using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Repositories;
using Entities.Accounting;
using Entities.Shop;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Services.BS.Contracts;
using WebFramework.Api;

namespace Moradi.Controllers.v1.Accounting
{
    /// <summary>
    /// کنترلر محصولات فاکتور
    /// </summary>
    [ApiVersion("1")]
    public class CartProductController : BaseApiController<CartProduct>
    {
        private readonly IRepository<CartProduct> repository;
        private readonly IRepository<ShopProduct> shop;
        ILogger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public CartProductController(IRepository<CartProduct> repository, IRepository<ShopProduct> _shop) : base(repository)
        {
            this.repository = repository;
            this.shop = _shop;
        }

        /// <summary>
        /// افزودن یک موجودیت به جدول
        /// </summary>
        /// <param name="entity">موجودیت</param>
        /// <param name="saveNow">ذخیره فوری</param>
        /// <returns>مدل موجودیت ایجاد شده</returns>
        [HttpPost]
        new public async Task<ApiResult<CartProduct>> Add(CartProduct entity, bool saveNow = true)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(entity));

            var exist = await shop.GetByIdAsync(new CancellationToken(), entity.shopProductId);

            if (exist != null)
            {
                entity.totalPrice = (exist.price * entity.qty) + entity.colorPrice + entity.warrentyPrice;

                return await repository.AddAsync(entity, new CancellationToken(), saveNow);
            }

            return new ApiResult<CartProduct>(false, Common.ApiResultStatusCode.NotFound, null);
        }

        /// <summary>
        /// بروزرسانی موجودیت
        /// </summary>
        /// <param name="entity">موجودیت</param>
        /// <param name="saveNow">ذخیره فوری</param>
        /// <returns>موجودیت ویرایش شده</returns>
        [HttpPut]
        new public async Task<ApiResult<CartProduct>> Update(CartProduct entity, bool saveNow = true)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(entity));

            var exist = await shop.GetByIdAsync(new CancellationToken(), entity.shopProductId);

            if (exist != null)
            {
                entity.totalPrice = (exist.price * entity.qty) + entity.colorPrice + entity.warrentyPrice;

                return await repository.UpdateAsync(entity, new CancellationToken(), saveNow);
            }

            return new ApiResult<CartProduct>(false, Common.ApiResultStatusCode.NotFound, null);
        }
    }
}
