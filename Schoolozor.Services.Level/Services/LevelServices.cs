using Schoolozor.Model;
using Schoolozor.Model.ViewModel.SchoolViewModels;
using Schoolozor.Shared.Model;
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
        private readonly SchoolContext _context;
        public LevelServices(IDataManager<Model.SchoolLevel> manager, IDataManager<Model.SchoolYear> syManager, SchoolContext context)
        {
            _manager = manager;
            _syManager = syManager;
            _context = context;
        }

        public List<SchoolLevelViewModel> GetSchoolLevels()
        {
            return _manager.GetAll(o => o.SchoolYear, o => o.SchoolYear.School)
                .Select(o => new SchoolLevelViewModel()
                {
                    Id = o.Id,
                    Name = o.Name,
                    SchoolId = o.SchoolYear.School.Id,
                    SchoolYearId = o.SchoolYear.Id
                })
                .ToList();
        }
        public List<SchoolLevelViewModel> GetSchoolLevels(Guid schoolYearId)
        {
            return _manager.GetList(o => o.SchoolYear.Id == schoolYearId, o => o.SchoolYear, o => o.SchoolYear.School)
                .Select(o => new SchoolLevelViewModel()
                {
                    Id = o.Id,
                    Name = o.Name,
                    SchoolId = o.SchoolYear.School.Id,
                    SchoolYearId = o.SchoolYear.Id,
                    SchoolYear = o.SchoolYear.Name
                })
                .ToList();
        }
        public SchoolLevelViewModel GetSchoolLevel(Guid schoolLevelId)
        {
            var o = _manager.GetSingle(o => o.Id == schoolLevelId, o => o.SchoolYear, o => o.SchoolYear.School);
            return new SchoolLevelViewModel()
            {
                Id = o.Id,
                Name = o.Name,
                SchoolId = o.SchoolYear.School.Id,
                SchoolYearId = o.SchoolYear.Id
            };
                
        }
        public async Task<ResponseResult<SchoolLevelViewModel>> AddSchoolLevel(SchoolLevelViewModel data)
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                var sy = _syManager.GetSingle(o => o.Id == data.SchoolYearId);

                var level = await _manager.AddAsync(new SchoolLevel()
                {
                    Name = data.Name,
                    SchoolYear = sy
                });
                data.Id = level.Id;
                await trans.CommitAsync();
            }

            return ResponseResult<SchoolLevelViewModel>.SetSuccess(data);
        }
    }
}
