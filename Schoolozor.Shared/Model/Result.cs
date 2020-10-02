using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Schoolozor.Shared.Model
{
    public class ResponseResult<T>
    {
        public bool Succeeded { get; set; }
        public T Entity { get; set; }
        public Error Error { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static ResponseResult<T> SetSuccess(T data)
        {
            var model = new ResponseResult<T>
            {
                Succeeded = true,
                Entity = data,
                Error = null
            };
            Log.Information(Newtonsoft.Json.JsonConvert.SerializeObject(model));
            return model;
        }
        public static ResponseResult<T> SetError(string description, HttpStatusCode statusCode)
        {
            var model = new ResponseResult<T>
            {
                Succeeded = false,
                Entity = default(T),
                Error = new Error
                {
                    Description = description,
                    Type = statusCode
                }
            };
            Log.Information(Newtonsoft.Json.JsonConvert.SerializeObject(model));
            return model;
        }
        public static ResponseResult<T> SetError(T data, string description, HttpStatusCode statusCode)
        {
            var model = new ResponseResult<T>
            {
                Succeeded = false,
                Entity = data,
                Error = new Error
                {
                    Description = description,
                    Type = statusCode
                }
            };
            Log.Information(Newtonsoft.Json.JsonConvert.SerializeObject(model));
            return model;
        }
    }
}
