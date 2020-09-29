using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolozor.Model
{
    public class SchoolLevel : BaseContextFields
    {
        public string Name { get; set; }
        public virtual SchoolProfile School { get; set; }
    }
}
