using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Schoolozor.Model;
using Schoolozor.Model.ViewModel.SchoolViewModels;
using Schoolozor.Services.Base.Common;
using Schoolozor.Services.Base.Controllers;
using Schoolozor.Services.Records.Services;
using Schoolozor.Services.Teacher.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolozor.Services.Teacher.Controllers
{
    [Authorize(Roles = "Admin,Staff,Teacher")]
    public class TeacherController : BaseController
    {
        private readonly TeacherServices _teacher;
        private readonly RecordsServices _records;
        public TeacherController(TeacherServices teacher, RecordsServices records, UserManager<SchoolUser> userManager) : base(userManager)
        {
            _teacher = teacher;
            _records = records;
        }
        [Route("teacher/{schoolId}")]
        public IActionResult Index()
        {
            AddPageHeader("Teachers");
            return View();
        }

        public async Task<IActionResult> NewData()
        {
            var user = await CurrentUser();

            AddPageHeader("Teachers");
            AddBreadcrumb("Teachers", "/teacher/" + user.School.Id);
            AddBreadcrumb("[New]", "");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewData(SchoolTeacherViewModel data)
        {
            var user = await CurrentUser();
            if (Validate())
            {
                var result = await _teacher.AddTeacher(data, user.School);
                if (result.Succeeded)
                {
                    return RedirectToAction("Success", new { message = "Creating new teacher " + data.FullName + " succeeded!" });
                }
                else
                {
                    AddPageAlerts(PageAlertType.Warning, result.Error.Description);
                }
            }

            AddPageHeader("Teachers");
            AddBreadcrumb("Teachers", "/teacher/" + user.School.Id);
            AddBreadcrumb("[New]", "");

            return View();
        }
        public async Task<IActionResult> EditData(Guid Id)
        {
            var user = await CurrentUser();
            var data = _teacher.GetTeacher(Id);

            AddPageHeader("Teachers");
            AddBreadcrumb("Teachers", "/teacher/" + user.School.Id);
            AddBreadcrumb(data.FullName, "");
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> EditData(SchoolTeacherViewModel data)
        {
            var user = await CurrentUser();
            if (Validate())
            {
                var result = await _teacher.UpdateTeacher(data, user.School);
                if (result.Succeeded)
                {
                    return RedirectToAction("Success", new { message = "Updating teacher " + data.FullName + " succeeded!" });
                }
                else
                {
                    AddPageAlerts(PageAlertType.Warning, result.Error.Description);
                }
            }
            AddPageHeader("Teachers");
            AddBreadcrumb("Teachers", "/teacher/" + user.School.Id);
            AddBreadcrumb(data.FullName, "");
            return View(data);
        }
        public async Task<IActionResult> Success(string message)
        {
            AddPageAlerts(PageAlertType.Success, message);
            var user = await CurrentUser();

            AddPageHeader("Teachers");
            AddBreadcrumb("Teachers", "/teacher/" + user.School.Id);
            AddBreadcrumb("Success", "/teacher/" + user.School.Id);
            return View();
        }
        public async Task<IActionResult> GetData()
        {
            try
            {
                var user = await CurrentUser();
                var teachers = _teacher.GetTeachers(user.School.Id);
                return this.BuildDataTableForm<SchoolTeacherViewModel>(teachers);
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
