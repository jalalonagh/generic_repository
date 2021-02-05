using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using Entities.Accounting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.BS.Contracts;

namespace Moradi.Controllers.v1.Accounting
{
    /// <summary>
    /// کنترلر حساب بانکی
    /// </summary>
    [ApiVersion("1")]
    public class NationalCurrencyController : BaseApiController<NationalCurrency>
    {
        private readonly IRepository<NationalCurrency> repository;
        public NationalCurrencyController(IRepository<NationalCurrency> repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
