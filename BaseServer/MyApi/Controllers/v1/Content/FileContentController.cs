using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.FileExtensions;
using Common.Utilities;
using Data.Repositories;
using Entities.Base;
using Entities.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.BS.Contracts;
using WebFramework.Api;

namespace Moradi.Controllers.v1.Base
{
    /// <summary>
    /// کنترلر محتوا
    /// </summary>
    [ApiVersion("1")]
    public class FileContentController : BaseApiController<FileContent>
    {
        private readonly IRepository<FileContent> repository;
        public FileContentController(IRepository<FileContent> repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpPost("[action]")]
        public async Task<ApiResult<IEnumerable<FileContent>>> UploadBox([FromForm] IFormFile[] files)
        {
            if (files != null && files.Count() > 0)
            {
                IEnumerable<FileContent> uploadedFiles = new List<FileContent>();

                foreach (var item in files)
                {
                    var filename = item.GetRandomName();

                    var result = await repository.AddAsync(new FileContent()
                    {
                        name = filename,
                        path = ""
                    }, new CancellationToken());

                    var save = await item.AutoSaveAsync(HttpContext.GetUploadRootPath(), filename, "gallery", result.Id.ToString());

                    if (!string.IsNullOrEmpty(save))
                    {
                        var url = Path.Combine(HttpContext.GetUploadWebPath(), result.Id.ToString(), DateTime.Now.ToPersianDateFolderName(), "gallery", filename);
                        url = url.Replace("\\", "/");

                        var res = await repository.UpdateFieldRangeAsync(new CancellationToken(), result.Id, new KeyValuePair<string, dynamic>("path", url));

                        uploadedFiles = uploadedFiles.Append(res);
                    }
                }

                return new ApiResult<IEnumerable<FileContent>>(true, ApiResultStatusCode.Success, uploadedFiles);
            }

            return new ApiResult<IEnumerable<FileContent>>(false, Common.ApiResultStatusCode.LogicError, null);
        }

        [HttpPost("[action]")]
        public async Task<ApiResult<IEnumerable<FileContent>>> MultiUploadBox(UploadModel model)
        {
            if (model != null && model.files != null && model.files.Count() > 0)
            {
                IEnumerable<FileContent> uploadedFiles = new List<FileContent>();

                foreach (var item in model.files)
                {
                    var filename = item.GetRandomName();

                    var result = await repository.AddAsync(new FileContent()
                    {
                        name = filename,
                        path = ""
                    }, new CancellationToken());

                    var save = await item.AutoSaveAsync(HttpContext.GetUploadRootPath(), filename, "gallery", result.Id.ToString());

                    if (!string.IsNullOrEmpty(save))
                    {
                        var url = Path.Combine(HttpContext.GetUploadWebPath(), result.Id.ToString(), DateTime.Now.ToPersianDateFolderName(), "gallery", filename);
                        url = url.Replace("\\", "/");

                        var res = await repository.UpdateFieldRangeAsync(new CancellationToken(), result.Id, new KeyValuePair<string, dynamic>("path", url));

                        uploadedFiles = uploadedFiles.Append(res);

                        return new ApiResult<IEnumerable<FileContent>>(true, ApiResultStatusCode.Success, uploadedFiles);
                    }
                }
            }

            return new ApiResult<IEnumerable<FileContent>>(false, Common.ApiResultStatusCode.LogicError, null);
        }
    }

    public class UploadModel
    {
        public IEnumerable<IFormFile> files { get; set; }
    }
}