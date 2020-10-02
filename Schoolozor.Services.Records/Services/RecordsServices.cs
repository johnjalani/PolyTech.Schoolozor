using Schoolozor.Model;
using Schoolozor.Model.ViewModel.SchoolViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schoolozor.Services.Records.Services
{
    public class RecordsServices
    {
        private readonly IDataManager<Model.StudentRecord> _manager;
        private readonly SchoolContext _context;
        public RecordsServices(IDataManager<Model.StudentRecord> manager, SchoolContext context)
        {
            _manager = manager;
            _context = context;
        }
        public List<StudentRecordsViewModel> GetStudentRecords(Guid adviserId)
        {
            return _manager.GetList(o => o.Adviser.Id == adviserId, o => o.Adviser, o => o.SchoolYear, o => o.Level, o => o.Section)
                .Select(o => new StudentRecordsViewModel()
                {
                    Id = o.Id,
                    SectionId = o.Section == null ? Guid.Empty : o.Section.Id,
                    Section = o.Section?.Name,
                    LevelId = o.Level == null ? Guid.Empty : o.Level.Id,
                    Level = o.Level?.Name,
                    SchoolYearId = o.SchoolYear.Id,
                    SchoolYear = o.SchoolYear.Name,
                    AdviserId = o.Adviser == null ? Guid.Empty : o.Adviser.Id,
                    Adviser = o.Adviser == null ? null : $"{o.Adviser.FirstName} {o.Adviser.MiddleName} {o.Adviser.LastName}",
                    GPA = o.GPA                    
                }).ToList();
        }
        public List<StudentRecordsViewModel> GetStudentRecords(Guid adviserId, Guid schoolYearId)
        {
            return _manager.GetList(o => o.Adviser.Id == adviserId && o.SchoolYear.Id == schoolYearId, o => o.Adviser, o => o.SchoolYear, o => o.Level, o => o.Section)
                .Select(o => new StudentRecordsViewModel()
                {
                    Id = o.Id,
                    SectionId = o.Section == null ? Guid.Empty : o.Section.Id,
                    Section = o.Section?.Name,
                    LevelId = o.Level == null ? Guid.Empty : o.Level.Id,
                    Level = o.Level?.Name,
                    SchoolYearId = o.SchoolYear.Id,
                    SchoolYear = o.SchoolYear.Name,
                    AdviserId = o.Adviser == null ? Guid.Empty : o.Adviser.Id,
                    Adviser = o.Adviser == null ? null : $"{o.Adviser.FirstName} {o.Adviser.MiddleName} {o.Adviser.LastName}",
                    GPA = o.GPA
                }).ToList();
        }
    }
}
