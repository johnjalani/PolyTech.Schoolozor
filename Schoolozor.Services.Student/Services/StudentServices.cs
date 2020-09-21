using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Schoolozor.Model;
using Schoolozor.Model.ViewModel.SchoolViewModels;
using Schoolozor.Services.Records.Services;
using Schoolozor.Services.SchoolYear.Services;
using Schoolozor.Shared;
using Schoolozor.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolozor.Services.Student.Services
{
    public class StudentServices
    {
        private readonly IDataManager<StudentProfile> _manager;
        private readonly IDataManager<StudentAddress> _addressManager;
        private readonly IDataManager<SchoolTeacher> _teacherManager;
        private readonly SchoolYearServices _sy;
        private readonly RecordsServices _records;
        private readonly UserManager<SchoolUser> _userManager;
        private readonly SchoolContext _context;
        private readonly GeneralSettings _settings;
        public StudentServices(
                IDataManager<StudentProfile> manager,
                IDataManager<StudentAddress> addressManager,
                IDataManager<SchoolTeacher> teacherManager,
                SchoolYearServices sy,
                RecordsServices records,
                SchoolContext context,
                UserManager<SchoolUser> userManager,
                IOptions<GeneralSettings> settings)
        {
            _manager = manager;
            _context = context;
            _userManager = userManager;
            _settings = settings.Value;
            _addressManager = addressManager;
            _teacherManager = teacherManager;
            _sy = sy;
            _records = records;
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
        public List<StudentViewModel> GetStudents(SchoolUser user)
        {
            IList<StudentProfile> students = _manager.GetList(o => o.School.Id == user.School.Id && o.DeletedDateTime == null, o => o.School, o => o.User, o => o.Records);
            var currentSchoolYear = _sy.GetCurrentSchoolYear(user.School.Id);
            switch (user.Type)
            {
                case UserType.Admin:

                    break;
                case UserType.Clerk:

                    break;
                case UserType.Teacher:
                    var adviser = _teacherManager.GetSingle(o => o.User.Id == user.Id, o => o.User);
                    var records = _records.GetStudentRecords(adviser.Id, currentSchoolYear.Id);
                    students = students.Where(o=>o.Records.Select(i=>i.Id).Intersect(records.Select(i=>i.Id)).Any()).ToList();
                                
                    break;
                case UserType.Parent:
                    students = new List<StudentProfile>();
                    break;
                case UserType.Student:
                    students = new List<StudentProfile>();
                    break;
            }


            return students.Select(o => new StudentViewModel()
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
            }).ToList();
        }
        public StudentViewModel GetStudent(Guid studentId)
        {
            var o = _manager.GetSingle(o => o.Id == studentId && o.DeletedDateTime == null, o => o.School, o => o.User, o => o.CurrentAddress, o => o.PermanentAddress);
            return new StudentViewModel()
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
                UserId = Guid.Parse(o.User.Id),
                CurrentAddress = o.CurrentAddress,
                PermanentAddress = o.PermanentAddress,
            };
        }
        public async Task<ResponseResult<StudentViewModel>> AddStudent(StudentViewModel data, SchoolProfile school)
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                var r = Shared.String.GenerateRandom(_settings.StudentIdLength, false, true, false, false);
                var studentId = $"{school.Code}{r}";
                //check if studentID already exist
                while (_manager.GetList(o => o.StudentId == studentId).Count() > 0)
                {
                    r = Shared.String.GenerateRandom(_settings.StudentIdLength, false, true, false, false);
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
                    Type = UserType.Student,
                    InsertedDateTime = DateTime.Now,
                    UpdatedDateTime = DateTime.Now
                };

                var password = $"{data.LastName.ToProperCase()}{r}!";
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Student");

                    var student = await _manager.AddAsync(new Model.StudentProfile()
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

                        School = school,
                        User = user
                    });

                    data.Id = student.Id;
                    await trans.CommitAsync();
                }
            }

            return ResponseResult<StudentViewModel>.SetSuccess(data);
        }

        public async Task<ResponseResult<StudentViewModel>> UpdateStudent(StudentViewModel data, SchoolProfile school)
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                if (data.CurrentAddress.Id == Guid.Empty)
                {
                    data.CurrentAddress = await _addressManager.AddAsync(new StudentAddress()
                    {
                        Address = data.CurrentAddress.Address,
                        City = data.CurrentAddress.City,
                        Zip = data.CurrentAddress.Zip,
                        Country = data.CurrentAddress.Country
                    });
                }
                else
                {
                    data.CurrentAddress = await _addressManager.UpdateAsync(new StudentAddress()
                    {
                        Id = data.CurrentAddress.Id,
                        Address = data.CurrentAddress.Address,
                        City = data.CurrentAddress.City,
                        Zip = data.CurrentAddress.Zip,
                        Country = data.CurrentAddress.Country
                    });
                }

                if (data.PermanentAddress.Id == Guid.Empty)
                {
                    data.PermanentAddress = await _addressManager.AddAsync(new StudentAddress()
                    {
                        Address = data.PermanentAddress.Address,
                        City = data.PermanentAddress.City,
                        Zip = data.PermanentAddress.Zip,
                        Country = data.PermanentAddress.Country
                    });
                }
                else
                {
                    data.PermanentAddress = await _addressManager.UpdateAsync(new StudentAddress()
                    {
                        Id = data.PermanentAddress.Id,
                        Address = data.PermanentAddress.Address,
                        City = data.PermanentAddress.City,
                        Zip = data.PermanentAddress.Zip,
                        Country = data.PermanentAddress.Country
                    });
                }

                await _manager.UpdateAsync(new StudentProfile()
                {
                    Id = data.Id,
                    StudentId = data.StudentId,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    MiddleName = data.MiddleName,
                    DOB = data.DOB,
                    Email = data.Email,
                    Gender = data.Gender,
                    Mobile = data.Mobile,
                    Phone = data.Phone,
                    CurrentAddress = data.CurrentAddress,
                    PermanentAddress = data.PermanentAddress
                });

                await trans.CommitAsync();
            }

            return ResponseResult<StudentViewModel>.SetSuccess(data);
        }
    }
}
