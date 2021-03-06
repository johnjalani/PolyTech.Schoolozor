﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Schoolozor.Model
{
    public class StudentRecord : BaseContextFields
    {
        public virtual SchoolSection Section { get; set; }
        public virtual SchoolLevel Level { get; set; }
        public virtual SchoolYear SchoolYear { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal GPA { get; set; }
        public virtual IList<StudentAcademicActivity> AcademicActivities { get; set; }
        public virtual SchoolTeacher Adviser { get; set; }
    }
}
