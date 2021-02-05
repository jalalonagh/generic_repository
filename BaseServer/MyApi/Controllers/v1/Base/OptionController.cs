﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using Entities.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.BS.Contracts;

namespace Moradi.Controllers.v1.Base
{
    /// <summary>
    /// کنترلر تنظیمات
    /// </summary>
    [ApiVersion("1")]
    public class OptionController : BaseApiController<Option>
    {
        private readonly IRepository<Option> repository;
        public OptionController(IRepository<Option> repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}