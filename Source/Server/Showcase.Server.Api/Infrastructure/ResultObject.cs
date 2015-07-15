using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Showcase.Server.Api.Infrastructure
{
    public class ResultObject<T> where T : class
    {
        public ResultObject(T data)
        {
            this.Success = true;
            this.ErrorMessage = null;
            this.Data = data;
        }

        public ResultObject(bool success, string errorMessage, T data = null)
        {
            this.Success = success;
            this.ErrorMessage = errorMessage;
            this.Data = data;
        }

        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public T Data { get; set; }
    }
}