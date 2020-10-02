using System;
using System.Collections.Generic;

namespace Schoolozor.Model
{
    public class SchoolYear : BaseContextFields
    {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public virtual SchoolProfile School { get; set; }
    }
}
