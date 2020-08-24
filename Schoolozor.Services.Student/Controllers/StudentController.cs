using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Schoolozor.Model;
using Schoolozor.Services.Base.Common;
using Schoolozor.Services.Base.Controllers;
using Schoolozor.Services.Student.Services;
using System;
using System.Threading.Tasks;

namespace Schoolozor.Services.Student.Controllers
{
    [Authorize(Roles = "Admin,Staff,Teacher,Parent")]
    public class StudentController : BaseController
    {
        private readonly StudentServices _student;

        public StudentController(StudentServices student, UserManager<SchoolUser> userManager) : base(userManager)
        {
            _student = student;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetData()
        {
            try
            {
                var user = await CurrentUser();
                return this.BuildDataTableForm<StudentProfile>(_student.GetStudents(user.School.Id));
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
                return null;
            }
        }
    }
}
