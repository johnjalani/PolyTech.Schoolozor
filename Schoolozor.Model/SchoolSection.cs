using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolozor.Model
{
    public class SchoolSection : BaseDate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual SchoolYear SchoolYear { get; set; }
    }
}
