using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolozor.Model
{
    public class StudentMasterList : BaseContextFields
    {
        public string StudentId { get; set; }
        public virtual SchoolYear SchoolYear { get; set; }
    }
}
