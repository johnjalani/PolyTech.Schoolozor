using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Schoolozor.Model;
using Schoolozor.Model.ViewModel;
using Schoolozor.Services.Base.Common;
using Schoolozor.Services.SchoolYear.Services;
using Schoolozor.Services.Student.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolozorCore.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly StudentServices _student;
        private readonly SchoolYearServices _sy;
        private readonly UserManager<SchoolUser> _userManager;
        public SidebarViewComponent(StudentServices student, SchoolYearServices sy, UserManager<SchoolUser> userManager)
        {
            _sy = sy;
            _student = student;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke(string filter)
        {
            var userInfo = _userManager.FindByNameAsync(User.Identity.Name).Result;

            //you can do the access rights checking here by using session, user, and/or filter parameter
            var sidebars = new List<SidebarMenu>();

            sidebars.Add(ModuleHelper.AddModule("Home", "/", "fas fa-home"));
            sidebars.Add(ModuleHelper.AddModule("Calendar", "/calendar", "fas fa-calendar-alt"));
            if (User.IsInRole("Student"))
            {
                var userId = Guid.Parse(userInfo.Id);
                var studentProfile = _student.GetStudents(userInfo.School.Id).Where(o => o.UserId == userId).FirstOrDefault();
                sidebars.Add(ModuleHelper.AddModule("Profile", "/profile/readdata?Id=" + studentProfile.Id, "fas fa-user-graduate"));
                sidebars.Add(ModuleHelper.AddModule("Records", "/profile/records?Id=" + studentProfile.Id, "fas fa-server"));
            }

            if (User.IsInRole("Teacher"))
            {
                sidebars.Add(ModuleHelper.AddModule("Students", "/student", "fas fa-user-graduate"));
                sidebars.Add(ModuleHelper.AddTree("Manage", "fas fa-user-cog"));
                sidebars.Last().TreeChild = new List<SidebarMenu>()
                {
                    ModuleHelper.AddModule("Sections", "/section", "fas fa-people-arrows"),
                };

            }

            if (User.IsInRole("Admin") || User.IsInRole("Staff"))
            {
                var sy = _sy.GetCurrentSchoolYear(userInfo.School.Id);

                sidebars.Add(ModuleHelper.AddModule("Students", "/student", "fas fa-user-graduate"));
                sidebars.Add(ModuleHelper.AddTree("Manage", "fas fa-user-cog"));
                sidebars.Last().TreeChild = new List<SidebarMenu>()
                {
                    ModuleHelper.AddModule("Teachers", "/teacher/" + userInfo.School.Id, "fas fa-chalkboard-teacher"),
                    ModuleHelper.AddModule("School Years", "/schoolyear", "fas fa-book"),
                    ModuleHelper.AddModule("Levels", "/level/" + sy.Id, "fas fa-angle-double-up"),
                    ModuleHelper.AddModule("Sections", "/section", "fas fa-people-arrows"),
                };
                //sidebars.Add(ModuleHelper.AddModule("Admin", "/superadmin", "fas fa-user-cog"));
                //sidebars.Add(ModuleHelper.AddModule("Role", "/role", "fas fa-key"));
                //sidebars.Add(ModuleHelper.AddModule("Logs", "/userlogs", "fas fa-clipboard-list"));
            }

            return View(sidebars);
        }
    }
}
