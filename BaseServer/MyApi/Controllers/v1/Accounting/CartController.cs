using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Repositories;
using Entities.Accounting;
using Entities.Accounting.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using Services.BS.Contracts;
using WebFramework.Api;

namespace Moradi.Controllers.v1.Accounting
{
    /// <summary>
    /// کنترلر فاکتور ها
    /// </summary>
    [ApiVersion("1")]
    public class CartController : BaseApiController<Cart>
    {
        private readonly IRepository<Cart> repository;
        private readonly IRepository<Invoice> invoiceRepository;
        ILogger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public CartController(IRepository<Cart> repository, IRepository<Invoice> _invoiceRepository) : base(repository)
        {
            this.repository = repository;
            invoiceRepository = _invoiceRepository;
        }

        [HttpGet("[action]")]
        public async Task<ApiResult<Cart>> GetMyBasket(int userId, bool saveNow = true)
        {
            logger.Trace($"userId = {userId}");

            var inv = await repository.Entities
                .Where(w => w.userId == userId)
                .Include(i => i.additionalCosts)
                .Include(i => i.cartProducts)
                .FirstOrDefaultAsync();

            return new ApiResult<Cart>(true, Common.ApiResultStatusCode.Success, inv);
        }

        [HttpPut("[action]")]
        public async Task<ApiResult<Invoice>> GenerateInvoice(int cartId, string userName)
        {
            logger.Trace($"userName = {userName}, cartId = {cartId}");

            if (cartId > 0)
            {
                var result = await repository.GetByIdAsync(new CancellationToken(), cartId);

                if (result != null && result.user.UserName == userName)
                {
                    var invoiceData = result.ToInvoice();

                    var resultInvoice = await invoiceRepository.AddAsync(invoiceData, new CancellationToken());

                    return resultInvoice;
                }
            }

            return new ApiResult<Invoice>(false, Common.ApiResultStatusCode.NotFound, null);
        }

        [HttpDelete("[action]")]
        public async Task<ApiResult> DeleteUserCart(int userId)
        {
            logger.Trace($"userId = {userId}");

            if (userId > 0)
            {
                var result = await repository.Entities.Where(w => w.userId == userId).ToListAsync();

                await repository.DeleteRangeAsync(result, new CancellationToken());

                return new ApiResult<Invoice>(true, Common.ApiResultStatusCode.Success, null);
            }

            return new ApiResult<Invoice>(false, Common.ApiResultStatusCode.NotFound, null);
        }
    }
}
