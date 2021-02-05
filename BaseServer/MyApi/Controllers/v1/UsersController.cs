using Common;
using Common.FileExtensions;
using Common.Utilities;
using Data.Repositories;
using Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NLog;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace Moradi.Controllers.v1
{
    /// <summary>
    /// کنترلر کاربران
    /// </summary>
    [ApiVersion("1")]
    public class UsersController : BaseController
    {
        private readonly IUserRepository userRepository;
        private readonly IJwtService jwtService;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;
        private readonly SiteSettings siteSettings;
        ILogger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public UsersController(IUserRepository userRepository, IJwtService jwtService,
            UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.logger = logger;
            this.jwtService = jwtService;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            siteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }

        /// <summary>
        /// This method generate JWT Token
        /// </summary>
        /// <param name="tokenRequest">The information of token request</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        [AllowAnonymous]
        public virtual async Task<ActionResult> Token(string username, string password, CancellationToken cancellationToken)
        {
            logger.Trace($"userName = {username}, password = {password}");

            var result = await userRepository.Entities
                .Where(w => w.UserName == username)
                .Include(i => i.messages)
                .Include(i => i.invoices)
                .Include(i => i.comments)
                .Include(i => i.banks)
                .Include(i => i.addresses)
                .FirstOrDefaultAsync();

            if (result != null)
            {
                if (await userManager.CheckPasswordAsync(result, password))
                {
                    await signInManager.SignInAsync(result, false);
                    var token = await jwtService.GenerateAsync(result);
                    token.user = result;

                    return Ok(token);
                }
            }

            return Ok("");
        }

        [HttpGet("[action]")]
        public virtual async Task<ActionResult> ChangePassword(int _userId, string _current, string _new, string _repeat)
        {
            logger.Trace($"userId = {_userId}, caurrent password = {_current}, new passowrd = {_new}, repeat = {_repeat}");

            var user = await userRepository.Entities
                .Where(w => w.Id == _userId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user != null)
            {
                var response = await userManager.ChangePasswordAsync(user, _current, _new);
                if (response.Succeeded)
                {
                    return Ok(user);
                }
            }

            return Ok();
        }

        [HttpGet("[action]")]
        public virtual async Task<ActionResult> ResetPassword(string _email)
        {
            logger.Trace($"email = {_email}");

            var user = await userRepository.Entities
                .Where(w => w.Email == _email)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                var response = await userManager.ResetPasswordAsync(user, token, "1234567890");

                if (response.Succeeded)
                {
                    await SendMail(user, "reset password", "رمز جدید شما", "رمز جدید شما : 1234567890 میباشد" + Environment.NewLine + "فروشگاه اینترنتی دیجی پردیس");

                    return Ok(user);
                }
            }

            return Ok();
        }

        // additional actions
        /// <summary>
        /// افزودن یک موجودیت به جدول
        /// </summary>
        /// <param name="entity">موجودیت</param>
        /// <param name="saveNow">ذخیره فوری</param>
        /// <returns>مدل موجودیت ایجاد شده</returns>
        [HttpPost]
        public async Task<ApiResult<User>> Add(User entity, string password, bool saveNow = true)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(entity));
            logger.Trace($"password = {password}");

            var result = await userManager.CreateAsync(entity, password);

            return new ApiResult<User>(result.Succeeded, ApiResultStatusCode.Success, result.Succeeded ? entity : null);
        }

        ///// <summary>
        ///// افزودن دسته جمعی موجودیت
        ///// </summary>
        ///// <param name="entities">لیستی ا موجودیت ها</param>
        ///// <param name="saveNow">ذخیره فوری</param>
        ///// <returns>لیست موجودیت های افزوده شده</returns>
        //[HttpPost("[action]")]
        //public async Task<ApiResult<IEnumerable<User>>> AddRange(IEnumerable<User> entities, bool saveNow = true)
        //{
        //    return new ApiResult<IEnumerable<User>>(true, Common.ApiResultStatusCode.Success, await userRepository.AddRangeAsync(entities, new CancellationToken(), saveNow));
        //}

        /// <summary>
        /// حذف یم موجودیت
        /// </summary>
        /// <param name="entity">موجودیت مورد حذف</param>
        /// <param name="saveNow">ذخیره و برو رسانی فوری</param>
        /// <returns>مدل حذف شده</returns>
        [HttpDelete]
        public async Task<ApiResult<User>> Delete(User entity, bool saveNow = true)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(entity));

            return await userRepository.DeleteAsync(entity, new CancellationToken(), saveNow);
        }

        /// <summary>
        /// حذف یم موجودیت
        /// </summary>
        /// <param name="id">شناسه مورد حذف</param>
        /// <param name="saveNow">ذخیره و برو رسانی فوری</param>
        /// <returns>مدل حذف شده</returns>
        [HttpPost("[action]")]
        public async Task<ApiResult<User>> DeleteById(int id, bool saveNow = true)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(id));

            return await userRepository.DeleteByIdAsync(id, new CancellationToken(), saveNow);
        }

        /// <summary>
        /// حذف دسته جمعی موجودیت
        /// </summary>
        /// <param name="entities">لیست موجودیت ها</param>
        /// <param name="saveNow">ذخیره و بروزرسانی فوری</param>
        /// <returns>لیست موجودیت های حذف شده</returns>
        [HttpDelete("[action]")]
        public async Task<ApiResult<IEnumerable<User>>> DeleteRange(IEnumerable<User> entities, bool saveNow = true)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(entities));

            return new ApiResult<IEnumerable<User>>(true, Common.ApiResultStatusCode.Success, await userRepository.DeleteRangeAsync(entities, new CancellationToken(), saveNow));
        }

        /// <summary>
        /// حذف دسته جمعی موجودیت
        /// </summary>
        /// <param name="ids">لیست موجودیت ها</param>
        /// <param name="saveNow">ذخیره و بروزرسانی فوری</param>
        /// <returns>لیست موجودیت های حذف شده</returns>
        [HttpPost("[action]")]
        public async Task<ApiResult<IEnumerable<User>>> DeleteRangeByIds(IEnumerable<int> ids, bool saveNow = true)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(ids));

            return new ApiResult<IEnumerable<User>>(true, Common.ApiResultStatusCode.Success, await userRepository.DeleteRangeByIdsAsync(ids, new CancellationToken(), saveNow));
        }

        /// <summary>
        /// فیلتر کردن موجودیت ها
        /// </summary>
        /// <param name="entity">مدل مقدار دهی شده برای فیلتر</param>
        /// <param name="total">مقدار دریافت شده قبلی</param>
        /// <param name="more">تعداد خواندن موجودیت ها</param>
        /// <returns>لیست موجودیت های دریافتی</returns>
        [HttpPost("[action]")]
        public async Task<ApiResult<IEnumerable<User>>> FilterRange(User entity, int total = 0, int more = int.MaxValue)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(entity));
            logger.Trace($"total = {total}, more = {more}");

            return new ApiResult<IEnumerable<User>>(true, Common.ApiResultStatusCode.Success, await userRepository.FilterRangeAsync(entity, new CancellationToken(), total, more));
        }

        /// <summary>
        /// دریافت تمام موجودیت
        /// </summary>
        /// <param name="total">تعداد دریافت شده قبلی</param>
        /// <param name="more">تعداد مورد نیاز برای دریافت</param>
        /// <returns>لیست موجودیت های دریافت شده</returns>
        [HttpGet]
        public async Task<ApiResult<IEnumerable<User>>> GetAll(int total = 0, int more = int.MaxValue)
        {
            logger.Trace($"total = {total}, more = {more}");

            return new ApiResult<IEnumerable<User>>(true, Common.ApiResultStatusCode.Success, await userRepository.GetAllAsync(new CancellationToken(), total, more));
        }

        /// <summary>
        /// دریافت بر اساس شناسه موجودیت
        /// </summary>
        /// <param name="ids">لیست شناسه ها</param>
        /// <returns>موجودیت یافت شده</returns>
        [HttpGet("[action]")]
        public async Task<ApiResult<User>> GetById(int[] ids)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(ids));

            return await userRepository.GetByIdAsync(new CancellationToken(), ids);
        }

        /// <summary>
        /// جستجوی موجودیت ها بر اساس مدل ارسالی
        /// </summary>
        /// <param name="entity">مل موجودیت</param>
        /// <param name="text">متن جستجو</param>
        /// <param name="total">تعداد دریافت شده قبلی</param>
        /// <param name="more">تعداد مورد نیاز موجودیت </param>
        /// <returns>لیست موجودیت های دریافت شده</returns>
        [HttpPost("[action]")]
        public async Task<ApiResult<IEnumerable<User>>> SearchRange(User entity, string text, int total = 0, int more = int.MaxValue)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(entity));
            logger.Trace($"text = {text}, total = {total}, more = {more}");

            return new ApiResult<IEnumerable<User>>(true, Common.ApiResultStatusCode.Success, await userRepository.SearchRangeAsync(entity, text, new CancellationToken(), total, more));
        }

        /// <summary>
        /// بروزرسانی موجودیت
        /// </summary>
        /// <param name="entity">موجودیت</param>
        /// <param name="saveNow">ذخیره فوری</param>
        /// <returns>موجودیت ویرایش شده</returns>
        [HttpPut]
        public async Task<ApiResult<User>> Update(User entity, bool saveNow = true)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(entity));

            return await userRepository.UpdateAsync(entity, new CancellationToken(), saveNow);
        }

        /// <summary>
        /// بروزرسانی موجودیت بر اساس فیلد های آن
        /// </summary>
        /// <param name="entity">موجودیت</param>
        /// <param name="fields">نام فیلد های قابل ویرایش</param>
        /// <returns>موجودیت ویرایش شده</returns>
        [HttpPut("[action]")]
        public async Task<ApiResult<User>> UpdateFieldRange(User entity, [FromQuery] string[] fields)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(entity));
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(fields));

            return await userRepository.UpdateFieldRangeAsync(new CancellationToken(), entity, fields);
        }

        /// <summary>
        /// بروزرسانی موجودیت ها
        /// </summary>
        /// <param name="entities">موجودیت های قابل ویرایش</param>
        /// <param name="saveNow">ذخیره فوری</param>
        /// <returns>لیست موجودیت های ویرایش شده</returns>
        [HttpPut("[action]")]
        public async Task<ApiResult<IEnumerable<User>>> UpdateRange(IEnumerable<User> entities, bool saveNow = true)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(entities));

            return new ApiResult<IEnumerable<User>>(true, Common.ApiResultStatusCode.Success, await userRepository.UpdateRangeAsync(entities, new CancellationToken(), saveNow));
        }

        /// <summary>
        /// آپلود فایل و ذخیره در فیلد رکورد
        /// </summary>
        /// <param name="file">فایل ارسالی از سمت کاربر</param>
        /// <param name="id">شناسه رکورد</param>
        /// <param name="property">نام فیلد درون جدول</param>
        /// <param name="saveNow">ذخیره فوری</param>
        /// <returns>مدل ویرایش شده</returns>
        [HttpPost("[action]")]
        public async Task<ApiResult<User>> UploadFile(IFormFile file, int id, string property, bool saveNow = true)
        {
            if (file != null)
            {
                var filename = file.GetRandomName();

                var save = await file.AutoSaveAsync(HttpContext.GetUploadRootPath(), filename, "docs", id.ToString());

                if (!string.IsNullOrEmpty(save))
                {
                    var url = Path.Combine(HttpContext.GetUploadWebPath(), id.ToString(), DateTime.Now.ToPersianDateFolderName(), "docs", filename);
                    url = url.Replace("\\", "/");

                    return await userRepository.UpdateFieldRangeAsync(new CancellationToken(), id, new KeyValuePair<string, dynamic>(property, url));
                }

            }

            return new ApiResult<User>(false, ApiResultStatusCode.ServerError, null);
        }

        private async Task SendMail(User user, string token, string subject, string body)
        {
            SmtpClient client = new SmtpClient();
            MailAddress from = new MailAddress("info@digipardis.com",
               "فروشگاه دیجی پردیس",
            System.Text.Encoding.UTF8);
            MailAddress to = new MailAddress(user.Email, user.fullname);
            MailMessage message = new MailMessage(from, to);
            message.Body = body;
            message.Body += Environment.NewLine;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = subject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            client.SendAsync(message, token);
            message.Dispose();
        }
    }
}
