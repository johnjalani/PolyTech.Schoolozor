using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolozor.Model
{
    public class SchoolLevel : BaseDate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual SchoolYear SchoolYear { get; set; }
    }
}
