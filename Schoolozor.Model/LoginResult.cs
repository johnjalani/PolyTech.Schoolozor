using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolozor.Model
{
    public class LoginResult
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
