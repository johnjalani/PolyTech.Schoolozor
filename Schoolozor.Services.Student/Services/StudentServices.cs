using Microsoft.AspNetCore.Identity;
using Schoolozor.Model;
using Schoolozor.Model.ViewModel.SchoolViewModels;
using Schoolozor.Shared.Model;
using Schoolozor.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Schoolozor.Services.Student.Services
{
    public class StudentServices
    {
        private readonly IDataManager<StudentProfile> _manager;
        private readonly UserManager<SchoolUser> _userManager;
        private readonly SchoolContext _context;
        public StudentServices(
                IDataManager<StudentProfile> manager,
                SchoolContext context,
                UserManager<SchoolUser> userManager)
        {
            _manager = manager;
            _context = context;
            _userManager = userManager;
        }
        public List<StudentViewModel> GetStudents()
        {
            return _manager.GetAll()
                           .Select(o => new StudentViewModel()
                           {
                               Id = o.Id,
                               DOB = o.DOB,
                               Email = o.Email,
                               FirstName = o.FirstName,
                               LastName = o.LastName,
                               MiddleName = o.MiddleName,
                               Gender = o.Gender,
                               Mobile = o.Mobile,
                               Phone = o.Phone,
                               SchoolId = o.School.Id,
                               StudentId = o.StudentId,
                               UserId = Guid.Parse(o.User.Id)
                           })
                           .ToList();
        }
        public List<StudentViewModel> GetStudents(Guid schoolId)
        {

            return _manager.GetList(o => o.School.Id == schoolId && o.DeletedDateTime == null, o => o.School, o => o.User)
                           .Select(o => new StudentViewModel()
                           {
                               Id = o.Id,
                               DOB = o.DOB,
                               Email = o.Email,
                               FirstName = o.FirstName,
                               LastName = o.LastName,
                               MiddleName = o.MiddleName,
                               Gender = o.Gender,
                               Mobile = o.Mobile,
                               Phone = o.Phone,
                               SchoolId = o.School.Id,
                               StudentId = o.StudentId,
                               UserId = Guid.Parse(o.User.Id)
                           })
                           .ToList();
        }

        public async Task<ResponseResult<StudentViewModel>> AddStudent(StudentViewModel data, SchoolProfile school)
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                var r = Shared.String.GenerateRandom(6, false, true, false, false);
                var studentId = $"{school.Code}{r}";
                //check if studentID already exist
                while (_manager.GetList(o=>o.StudentId == studentId).Count() > 0)
                {
                    r = Shared.String.GenerateRandom(6, false, true, false, false);
                    studentId = $"{school.Code}{r}";
                }
                var username = $"{studentId}@{school.Name.Replace(" ", "_")}.com";
                var user = new SchoolUser
                {
                    UserName = username,
                    Email = username,
                    EmailConfirmed = true,
                    //extended properties
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    School = school,
                    InsertedDateTime = DateTime.Now,
                    UpdatedDateTime = DateTime.Now
                };

                var password = $"{data.LastName.ToProperCase()}{r}!";
                var result = await _userManager.CreateAsync(user, password);
                
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Student");

                    await _manager.AddAsync(new Model.StudentProfile()
                    {
                        FirstName = data.FirstName,
                        LastName = data.LastName,
                        MiddleName = data.MiddleName,
                        Gender = data.Gender,
                        DOB = data.DOB,
                        Email = data.Email,
                        Phone = data.Phone,
                        Mobile = data.Mobile,
                        StudentId = studentId,

                        InsertedDateTime = DateTime.Now,
                        UpdatedDateTime = DateTime.Now,
                        School = school,
                        User = user
                    });

                     await trans.CommitAsync();
                }
            }

            return ResponseResult<StudentViewModel>.SetSuccess(data);
        }
    }
}
