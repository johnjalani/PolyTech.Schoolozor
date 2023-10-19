using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolozor.Model
{
    public class SchoolCalendar : BaseContextFields
    {
        public virtual SchoolYear SchoolYear { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
    }
}
