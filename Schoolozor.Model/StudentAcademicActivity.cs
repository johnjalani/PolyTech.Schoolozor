using System;
using System.Collections.Generic;
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
    public class StudentAcademicActivity
    {
        public Guid Id { get; set; }
        public GradingPeriod Period { get; set; }
        public ActivityType Type { get; set; }
        public string Name { get; set; }
        public decimal Score { get; set; }
        public DateTime InsertDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
