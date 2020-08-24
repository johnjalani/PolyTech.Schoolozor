using Schoolozor.Model;
using Schoolozor.Model.ViewModel.SchoolViewModels;
using Schoolozor.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolozor.Services.SchoolYear.Services
{
    public class SchoolYearServices
    {
        private readonly IDataManager<Model.SchoolYear> _manager;
        public SchoolYearServices(IDataManager<Model.SchoolYear> manager)
        {
            _manager = manager;
        }
        public List<SchoolYearViewModel> GetSchoolYears()
        {
            return _manager.GetAll()
                           .Select(o => new SchoolYearViewModel()
                           {
                               Id = o.Id,
                               Name = o.Name,
                               Start = o.Start,
                               End = o.End,
                               SchoolId = o.School.Id
                           })
                           .ToList();
        }
        public List<SchoolYearViewModel> GetSchoolYears(Guid schoolId)
        {
            return _manager.GetList(o => o.School.Id == schoolId && o.DeletedDateTime == null)
                           .Select(o => new SchoolYearViewModel()
                           {
                               Id = o.Id,
                               Name = o.Name,
                               Start = o.Start,
                               End = o.End,
                               SchoolId = o.School.Id
                           })
                           .ToList();
        }

        public SchoolYearViewModel GetSchoolYear(Guid schoolYearId)
        {
            var o = _manager.GetSingle(o => o.Id == schoolYearId && o.DeletedDateTime == null);

            return new SchoolYearViewModel()
            {
                Id = o.Id,
                Name = o.Name,
                Start = o.Start,
                End = o.End,
                SchoolId = o.School.Id
            };
        }

        public async Task<ResponseResult<SchoolYearViewModel>> AddSchoolYear(SchoolYearViewModel data, SchoolProfile school)
        {
            await _manager.AddAsync(new Model.SchoolYear()
            {
                Name = data.Name,
                Start = data.Start.Value,
                End = data.End.Value,
                InsertedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
                School = school
            });
            return ResponseResult<SchoolYearViewModel>.SetSuccess(data);
        }
    }
}
