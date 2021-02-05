using Common;
using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class ServiceResult
    {
        public bool IsSuccess { get; set; }
        public ApiResultStatusCode StatusCode { get; set; }
        public string Message { get; set; }

        public ServiceResult(bool isSuccess, ApiResultStatusCode statusCode, string message = null)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Message = message ?? statusCode.ToDisplay();
        }
    }

    public class ServiceResult<TData> : ServiceResult
        where TData : class
    {
        public TData Data { get; set; }

        public ServiceResult(bool isSuccess, ApiResultStatusCode statusCode, TData data, string message = null)
            : base(isSuccess, statusCode, message)
        {
            Data = data;
        }
        #region Implicit Operators
        public static implicit operator ServiceResult<TData>(TData data)
        {
            return new ServiceResult<TData>(true, ApiResultStatusCode.Success, data);
        }
        #endregion
    }
}
