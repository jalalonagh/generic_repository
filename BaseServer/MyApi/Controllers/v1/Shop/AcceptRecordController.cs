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
    /// کنترلر موارد تائید شده
    /// </summary>
    [ApiVersion("1")]
    public class AcceptRecordController : BaseApiController<AcceptRecord>
    {
        private readonly IRepository<AcceptRecord> repository;
        public AcceptRecordController(IRepository<AcceptRecord> repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
