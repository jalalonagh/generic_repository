using Common;
using Common.FileExtensions;
using Common.Utilities;
using Data.Repositories;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace Moradi.Controllers.v1
{
    [ApiVersion("1")]
    public class BaseApiController<TEntity> : BaseController
        where TEntity : class, IEntity
    {
        private readonly IRepository<TEntity> repository;
        ILogger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public BaseApiController(IRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// افزودن یک موجودیت به جدول
        /// </summary>
        /// <param name="entity">موجودیت</param>
        /// <param name="saveNow">ذخیره فوری</param>
        /// <returns>مدل موجودیت ایجاد شده</returns>
        [HttpPost]
        public async Task<ApiResult<TEntity>> Add(TEntity entity, bool saveNow = true)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(entity));

            return await repository.AddAsync(entity, new CancellationToken(), saveNow);
        }

        /// <summary>
        /// افزودن دسته جمعی موجودیت
        /// </summary>
        /// <param name="entities">لیستی ا موجودیت ها</param>
        /// <param name="saveNow">ذخیره فوری</param>
        /// <returns>لیست موجودیت های افزوده شده</returns>
        [HttpPost("[action]")]
        public async Task<ApiResult<IEnumerable<TEntity>>> AddRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(entities));

            return new ApiResult<IEnumerable<TEntity>>(true, Common.ApiResultStatusCode.Success, await repository.AddRangeAsync(entities, new CancellationToken(), saveNow));
        }

        /// <summary>
        /// حذف یم موجودیت
        /// </summary>
        /// <param name="entity">موجودیت مورد حذف</param>
        /// <param name="saveNow">ذخیره و برو رسانی فوری</param>
        /// <returns>مدل حذف شده</returns>
        [HttpDelete]
        public async Task<ApiResult<TEntity>> Delete(TEntity entity, bool saveNow = true)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(entity));

            return await repository.DeleteAsync(entity, new CancellationToken(), saveNow);
        }

        /// <summary>
        /// حذف یم موجودیت
        /// </summary>
        /// <param name="id">شناسه مورد حذف</param>
        /// <param name="saveNow">ذخیره و برو رسانی فوری</param>
        /// <returns>مدل حذف شده</returns>
        [HttpPost("[action]")]
        public async Task<ApiResult<TEntity>> DeleteById(int id, bool saveNow = true)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(id));

            return await repository.DeleteByIdAsync(id, new CancellationToken(), saveNow);
        }

        /// <summary>
        /// حذف دسته جمعی موجودیت
        /// </summary>
        /// <param name="entities">لیست موجودیت ها</param>
        /// <param name="saveNow">ذخیره و بروزرسانی فوری</param>
        /// <returns>لیست موجودیت های حذف شده</returns>
        [HttpDelete("[action]")]
        public async Task<ApiResult<IEnumerable<TEntity>>> DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(entities));

            return new ApiResult<IEnumerable<TEntity>>(true, Common.ApiResultStatusCode.Success, await repository.DeleteRangeAsync(entities, new CancellationToken(), saveNow));
        }

        /// <summary>
        /// حذف دسته جمعی موجودیت
        /// </summary>
        /// <param name="ids">لیست موجودیت ها</param>
        /// <param name="saveNow">ذخیره و بروزرسانی فوری</param>
        /// <returns>لیست موجودیت های حذف شده</returns>
        [HttpPost("[action]")]
        public async Task<ApiResult<IEnumerable<TEntity>>> DeleteRangeByIds(IEnumerable<int> ids, bool saveNow = true)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(ids));

            return new ApiResult<IEnumerable<TEntity>>(true, Common.ApiResultStatusCode.Success, await repository.DeleteRangeByIdsAsync(ids, new CancellationToken(), saveNow));
        }

        /// <summary>
        /// فیلتر کردن موجودیت ها
        /// </summary>
        /// <param name="entity">مدل مقدار دهی شده برای فیلتر</param>
        /// <param name="total">مقدار دریافت شده قبلی</param>
        /// <param name="more">تعداد خواندن موجودیت ها</param>
        /// <returns>لیست موجودیت های دریافتی</returns>
        [HttpPost("[action]")]
        public async Task<ApiResult<IEnumerable<TEntity>>> FilterRange(TEntity entity, int total = 0, int more = int.MaxValue)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(entity));

            return new ApiResult<IEnumerable<TEntity>>(true, Common.ApiResultStatusCode.Success, await repository.FilterRangeAsync(entity, new CancellationToken(), total, more));
        }

        /// <summary>
        /// دریافت تمام موجودیت
        /// </summary>
        /// <param name="total">تعداد دریافت شده قبلی</param>
        /// <param name="more">تعداد مورد نیاز برای دریافت</param>
        /// <returns>لیست موجودیت های دریافت شده</returns>
        [HttpGet]
        public async Task<ApiResult<IEnumerable<TEntity>>> GetAll(int total = 0, int more = int.MaxValue)
        {
            logger.Trace($"total = {total}, more = {more}");

            return new ApiResult<IEnumerable<TEntity>>(true, Common.ApiResultStatusCode.Success, await repository.GetAllAsync(new CancellationToken(), total, more));
        }

        /// <summary>
        /// دریافت بر اساس شناسه موجودیت
        /// </summary>
        /// <param name="ids">لیست شناسه ها</param>
        /// <returns>موجودیت یافت شده</returns>
        [HttpGet("[action]")]
        public async Task<ApiResult<TEntity>> GetById(int[] ids)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(ids));

            return await repository.GetByIdAsync(new CancellationToken(), ids);
        }

        /// <summary>
        /// دریافت بر اساس شناسه موجودیت
        /// </summary>
        /// <param name="ids">لیست شناسه ها</param>
        /// <returns>موجودیت یافت شده</returns>
        [HttpGet("[action]")]
        public async Task<ApiResult<IEnumerable<TEntity>>> FetchById(int id)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(id));

            var result = await repository.FetchByIdAsync(new CancellationToken(), id);

            return new ApiResult<IEnumerable<TEntity>>(true, ApiResultStatusCode.Success, result);
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
        public async Task<ApiResult<IEnumerable<TEntity>>> SearchRange(TEntity entity, string text, int total = 0, int more = int.MaxValue)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(entity));
            logger.Trace($"text = {text}, total = {total}, more = {more}");

            return new ApiResult<IEnumerable<TEntity>>(true, Common.ApiResultStatusCode.Success, await repository.SearchRangeAsync(entity, text, new CancellationToken(), total, more));
        }

        /// <summary>
        /// بروزرسانی موجودیت
        /// </summary>
        /// <param name="entity">موجودیت</param>
        /// <param name="saveNow">ذخیره فوری</param>
        /// <returns>موجودیت ویرایش شده</returns>
        [HttpPut]
        public async Task<ApiResult<TEntity>> Update(TEntity entity, bool saveNow = true)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(entity));

            return await repository.UpdateAsync(entity, new CancellationToken(), saveNow);
        }

        /// <summary>
        /// بروزرسانی موجودیت بر اساس فیلد های آن
        /// </summary>
        /// <param name="entity">موجودیت</param>
        /// <param name="fields">نام فیلد های قابل ویرایش</param>
        /// <returns>موجودیت ویرایش شده</returns>
        [HttpPut("[action]")]
        public async Task<ApiResult<TEntity>> UpdateFieldRange(TEntity entity, [FromQuery] string[] fields)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(entity));
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(fields));

            return await repository.UpdateFieldRangeAsync(new CancellationToken(), entity, fields);
        }

        /// <summary>
        /// بروزرسانی موجودیت ها
        /// </summary>
        /// <param name="entities">موجودیت های قابل ویرایش</param>
        /// <param name="saveNow">ذخیره فوری</param>
        /// <returns>لیست موجودیت های ویرایش شده</returns>
        [HttpPut("[action]")]
        public async Task<ApiResult<IEnumerable<TEntity>>> UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(entities));

            return new ApiResult<IEnumerable<TEntity>>(true, Common.ApiResultStatusCode.Success, await repository.UpdateRangeAsync(entities, new CancellationToken(), saveNow));
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
        public async Task<ApiResult<TEntity>> UploadFile(IFormFile file, int id, string property, bool saveNow = true)
        {
            if (file != null)
            {
                var filename = file.GetRandomName();

                var save = await file.AutoSaveAsync(HttpContext.GetUploadRootPath(), filename, "docs", id.ToString());

                if (!string.IsNullOrEmpty(save))
                {
                    var url = Path.Combine(HttpContext.GetUploadWebPath(), id.ToString(), DateTime.Now.ToPersianDateFolderName(), "docs", filename);
                    url = url.Replace("\\", "/");

                    return await repository.UpdateFieldRangeAsync(new CancellationToken(), id, new KeyValuePair<string, dynamic>(property, url));
                }

            }

            return new ApiResult<TEntity>(false, ApiResultStatusCode.ServerError, null);
        }
    }
}