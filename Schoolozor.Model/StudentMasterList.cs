using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolozor.Model
{
    public class StudentMasterList : BaseDate
    {
        public Guid Id { get; set; }
        public string StudentId { get; set; }
        public virtual SchoolYear SchoolYear { get; set; }
    }
}
