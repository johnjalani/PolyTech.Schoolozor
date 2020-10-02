using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Schoolozor.Model
{
    public enum ActivityType
    {
        Quiz,
        Exam,
        Periodical,
        Recitation,
        asdasd,

    }
    public enum GradingPeriod
    {
        First,
        Second,
        Third,
        Final
    }
    public class StudentAcademicActivity : BaseContextFields
    {
        public GradingPeriod Period { get; set; }
        public ActivityType Type { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Score { get; set; }
    }
}
