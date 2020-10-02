using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Schoolozor.Model;
using Schoolozor.Services.Base.Controllers;
using Schoolozor.Services.Student.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolozor.Services.Student.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly StudentServices _student;
        public ProfileController(StudentServices student, UserManager<SchoolUser> userManager) : base(userManager)
        {
            _student = student;
        }

        public IActionResult ReadData(Guid Id)
        {
            var data = _student.GetStudent(Id);

            AddPageHeader("Students");
            AddBreadcrumb("Students", "/student");
            AddBreadcrumb(data.StudentId, "");


            return View(data);
        }
    }
}
