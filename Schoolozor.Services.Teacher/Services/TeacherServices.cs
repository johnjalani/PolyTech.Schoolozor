using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Schoolozor.Model;
using Schoolozor.Model.ViewModel;
using Schoolozor.Model.ViewModel.SchoolViewModels;
using Schoolozor.Shared;
using Schoolozor.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolozor.Services.Teacher.Services
{
    public class TeacherServices
    {
        private readonly IDataManager<Model.SchoolTeacher> _manager;
        private readonly UserManager<SchoolUser> _userManager;
        private readonly SchoolContext _context;
        private readonly GeneralSettings _settings;
        public TeacherServices(IDataManager<Model.SchoolTeacher> manager, UserManager<SchoolUser> userManager, IOptions<GeneralSettings> settings, SchoolContext context)
        {
            _manager = manager;
            _context = context;
            _userManager = userManager;
            _settings = settings.Value;
        }
        public List<SchoolTeacherViewModel> GetTeachers(Guid schoolId)
        {
            var data =  _manager.GetList(o => o.User.School.Id == schoolId, o => o.User, o => o.User.School)
                .Select(o => new SchoolTeacherViewModel()
                {
                    Id = o.Id,
                    FirstName = o.FirstName,
                    MiddleName = o.MiddleName,
                    LastName = o.LastName,
                    Position = o.Position,
                    UserId = Guid.Parse(o.User.Id),
                    AssignedLevel = o.AssignedLevel == null ? new List<NameValuePair>() : o.AssignedLevel.Select(i=>new NameValuePair() { Name = i.Name, Value=i.Id }).ToList(),
                    AssignedSection = o.AssignedSection == null ? new List<NameValuePair>() : o.AssignedSection.Select(i => new NameValuePair() { Name = i.Name, Value = i.Id }).ToList()
                })
                .ToList();
            return data;
        }
        public async Task<ResponseResult<SchoolTeacherViewModel>> AddTeacher(SchoolTeacherViewModel data, SchoolProfile school)
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                var r = Shared.String.GenerateRandom(4, false, true, false, false);
                var username = $"{data.FirstName[0]}{data.LastName.ToLower()}_{r}@{school.Name.Replace(" ", "_")}.com";

                //check if studentID already exist
                while (_manager.GetList(o => o.User.UserName == username, o => o.User).Count() > 0)
                {
                    r = Shared.String.GenerateRandom(4, false, true, false, false);
                    username = $"{data.FirstName[0]}{data.LastName.ToLower()}_{r}@{school.Name.Replace(" ", "_")}.com";
                }

                var user = new SchoolUser
                {
                    UserName = username,
                    Email = username,
                    EmailConfirmed = true,
                    //extended properties
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    School = school,
                    Type = UserType.Teacher,
                    InsertedDateTime = DateTime.Now,
                    UpdatedDateTime = DateTime.Now,

                };

                var password = $"{data.LastName.ToProperCase()}{r}!";
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Teacher");

                    var teacher = await _manager.AddAsync(new SchoolTeacher()
                    {
                        FirstName = data.FirstName,
                        MiddleName = data.MiddleName,
                        LastName = data.LastName,
                        Position = data.Position,
                        User = user
                    });
                    data.Id = teacher.Id;
                    await trans.CommitAsync();
                }
            }

            return ResponseResult<SchoolTeacherViewModel>.SetSuccess(data);
        }
    }
}
