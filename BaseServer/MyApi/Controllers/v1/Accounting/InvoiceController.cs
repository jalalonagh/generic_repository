using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Repositories;
using Entities.Accounting;
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
    public class InvoiceController : BaseApiController<Invoice>
    {
        private readonly IRepository<Invoice> repository;
        ILogger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public InvoiceController(IRepository<Invoice> repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet("[action]")]
        public async Task<ApiResult<Invoice>> GetMyBasket(int userId, bool saveNow = true)
        {
            logger.Trace($"userId = {userId}");

            var inv = await repository.Entities
                .Where(w => w.userId == userId)
                .Include(i => i.additionalCosts)
                .Include(i => i.invoiceProducts)
                .Include(i => i.recipts)
                .FirstOrDefaultAsync();

            return new ApiResult<Invoice>(true, Common.ApiResultStatusCode.Success, inv);
        }
    }
}
