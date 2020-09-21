using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolozor.Model
{
    public class SchoolSection : BaseContextFields
    {
        public string Name { get; set; }
        public virtual SchoolYear SchoolYear { get; set; }
    }
}
