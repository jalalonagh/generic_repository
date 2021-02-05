using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repositories;
using Entities.Base;
using Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.BS.Contracts;

namespace Moradi.Controllers.v1
{
    /// <summary>
    /// کنترلر پیام های کاربران
    /// </summary>
    [ApiVersion("1")]
    public class UserAddressController : BaseApiController<UserAddress>
    {
        private readonly IRepository<UserAddress> repository;
        public UserAddressController(IRepository<UserAddress> repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}