using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolozor.Model
{
    public class StudentRecord
    {
        public Guid Id { get; set; }
        public int Level { get; set; }
        public string SchoolYear { get; set; }
        public decimal GPA { get; set; }
        public List<StudentAcademicActivity> AcademicActivities { get; set; }
        public DateTime InsertDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
