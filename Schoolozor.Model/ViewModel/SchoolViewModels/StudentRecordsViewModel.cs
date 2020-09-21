using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolozor.Model.ViewModel.SchoolViewModels
{
    public class StudentRecordsViewModel
    {
        public Guid Id { get; set; }
        public Guid SectionId { get; set; }
        public string Section { get; set; }
        public Guid LevelId { get; set; }
        public string Level { get; set; }
        public Guid SchoolYearId { get; set; }
        public string SchoolYear { get; set; }
        public decimal GPA { get; set; }
        public Guid AdviserId { get; set; }
        public string Adviser { get; set; }
        public Guid StudentId { get; set; }
        public string Student { get; set; }

    }
}
