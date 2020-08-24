using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolozor.Model
{
    public class SchoolCalendar : BaseDate
    {
        public Guid Id { get; set; }
        public virtual SchoolYear SchoolYear { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
