using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Schoolozor.Shared.Model
{
    public class Error
    {
        public int Code { get; set; }
        public HttpStatusCode Type { get; set; }
        public string Description { get; set; }
    }
}
