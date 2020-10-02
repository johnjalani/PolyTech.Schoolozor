using Schoolozor.Model;
using Schoolozor.Model.ViewModel.SchoolViewModels;
using Schoolozor.Shared.Model;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolozor.Services.Level.Services
{
    public class LevelServices
    {
        private readonly IDataManager<Model.SchoolLevel> _manager;
        private readonly IDataManager<Model.SchoolYear> _syManager;
        private readonly IDataManager<Model.SchoolProfile> _schoolManager;
        private readonly SchoolContext _context;
        public LevelServices(IDataManager<Model.SchoolLevel> manager, IDataManager<Model.SchoolYear> syManager, IDataManager<Model.SchoolProfile> schoolManager, SchoolContext context)
        {
            _manager = manager;
            _syManager = syManager;
            _schoolManager = schoolManager;
            _context = context;
        }

        public List<SchoolLevelViewModel> GetSchoolLevels()
        {
            return _manager.GetAll(o => o.School)
                .Select(o => new SchoolLevelViewModel()
                {
                    Id = o.Id,
                    Name = o.Name,
                    SchoolId = o.School.Id
                })
                .ToList();
        }
        public List<SchoolLevelViewModel> GetSchoolLevels(Guid schoolId)
        {
            return _manager.GetList(o => o.School.Id == schoolId, o => o.School)
                .Select(o => new SchoolLevelViewModel()
                {
                    Id = o.Id,
                    Name = o.Name,
                    SchoolId = o.School.Id,
                })
                .ToList();
        }
        public SchoolLevelViewModel GetSchoolLevel(Guid schoolLevelId)
        {
            var o = _manager.GetSingle(o => o.Id == schoolLevelId, o => o.School);
            return new SchoolLevelViewModel()
            {
                Id = o.Id,
                Name = o.Name,
                SchoolId = o.School.Id
            };
                
        }
        public async Task<ResponseResult<SchoolLevelViewModel>> AddSchoolLevel(SchoolLevelViewModel data)
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                var school = _schoolManager.GetSingle(o => o.Id == data.SchoolId);

                var level = await _manager.AddAsync(new SchoolLevel()
                {
                    Name = data.Name,
                    School = school
                });
                data.Id = level.Id;
                await trans.CommitAsync();
            }

            return ResponseResult<SchoolLevelViewModel>.SetSuccess(data);
        }
        public async Task<ResponseResult<SchoolLevelViewModel>> UpdateSchoolLevel(SchoolLevelViewModel data)
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                var level = _manager.GetSingle(o => o.Id == data.Id);
                level.Name = data.Name;
                await _manager.UpdateAsync(level);
                await trans.CommitAsync();
            }

            return ResponseResult<SchoolLevelViewModel>.SetSuccess(data);
        }
    }
}
