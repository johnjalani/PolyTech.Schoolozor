using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolozor.Model
{
    public class StudentAddress : BaseDate
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Zip { get; set; }
    }
}
