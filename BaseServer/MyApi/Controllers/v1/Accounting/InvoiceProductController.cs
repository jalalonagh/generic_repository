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
    /// کنترلر محصولات فاکتور
    /// </summary>
    [ApiVersion("1")]
    public class InvoiceProductController : BaseApiController<InvoiceProduct>
    {
        private readonly IRepository<InvoiceProduct> repository;
        public InvoiceProductController(IRepository<InvoiceProduct> repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
