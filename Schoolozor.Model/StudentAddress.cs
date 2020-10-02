using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolozor.Model
{
    public class StudentAddress : BaseContextFields
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
    }
}
