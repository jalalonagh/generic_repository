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
    /// کنترلر رسید پرداخت
    /// </summary>
    [ApiVersion("1")]
    public class PaymentReciptController : BaseApiController<PaymentRecipt>
    {
        private readonly IRepository<PaymentRecipt> repository;
        public PaymentReciptController(IRepository<PaymentRecipt> repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
