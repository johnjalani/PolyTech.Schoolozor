using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Schoolozor.Model;
using Schoolozor.Model.ViewModel.SchoolViewModels;
using Schoolozor.Services.Base.Common;
using Schoolozor.Services.Base.Controllers;
using Schoolozor.Services.Student.Services;
using System;
using System.Linq;
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
            AddPageHeader("Students");
            return View();
        }
        public IActionResult NewData()
        {
            AddPageHeader("Students");
            AddBreadcrumb("Students", "/student");
            AddBreadcrumb("[New]", "");

            AddPageAlerts(PageAlertType.Info, "Note: More information will be needed later in the edit student screen e.g. Addresses, Parents Details, etc");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewData(StudentViewModel data)
        {

            if (Validate())
            {
                var user = await CurrentUser();
                if (!Validate(data, user))
                {
                    return View();
                }

                var result = await _student.AddStudent(data, user.School);
                if (result.Succeeded)
                {
                    return RedirectToAction("success", new { message = "Creating new student " + data.FullName + " succeeded!" });
                }
                else
                {
                    AddPageAlerts(PageAlertType.Warning, result.Error.Description);
                }
            }

            return View();
        }

        public IActionResult Success(string message)
        {
            AddPageAlerts(PageAlertType.Success, message);
            AddPageHeader("Students");
            AddBreadcrumb("Students", "/student");
            return View();
        }

        public async Task<IActionResult> GetData()
        {
            try
            {
                var user = await CurrentUser();
                return this.BuildDataTableForm<StudentViewModel>(_student.GetStudents(user.School.Id));
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
                return null;
            }
        }

        private bool Validate(StudentViewModel data, SchoolUser user)
        {
            var IsValid = true;

            var st = _student.GetStudents(user.School.Id);
            foreach (var s in st.Where(o=>o.Id != data.Id))
            {
                if (data.FullName.Trim() == s.FullName.Trim() && data.DOB == s.DOB)
                {
                    AddPageAlerts(PageAlertType.Warning, "This student already exist!");
                    IsValid = false;
                    break;
                }
                if ((DateTime.Now.Year - data.DOB.Year) < 2)
                {
                    AddPageAlerts(PageAlertType.Warning, "This student is too young to go to school!");
                    IsValid = false;
                    break;
                }
            }

            return IsValid;
        }
    }
}
