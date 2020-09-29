using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Schoolozor.Model;
using Schoolozor.Model.ViewModel.SchoolViewModels;
using Schoolozor.Services.Base.Common;
using Schoolozor.Services.Base.Controllers;
using Schoolozor.Services.Level.Services;
using Schoolozor.Services.SchoolYear.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolozor.Services.Level.Controllers
{
    [Authorize(Roles = "Admin,Staff,Teacher,Parent")]
    public class LevelController : BaseController
    {
        private readonly LevelServices _lvl;
        private readonly SchoolYearServices _sy;
        public LevelController(LevelServices lvl, SchoolYearServices sy, UserManager<SchoolUser> userManager) : base(userManager)
        {
            _lvl = lvl;
            _sy = sy;
        }
        [Route("level/{schoolId}")]
        public IActionResult Index(Guid schoolId)
        {
            AddPageHeader("Levels");
            return View();
        }
        public async Task<IActionResult> NewData()
        {
            var user = await CurrentUser();

            AddPageHeader("Levels");
            AddBreadcrumb("Levels", "/level/" + user.School.Id);
            AddBreadcrumb("[New]", "");

            
            var model = new SchoolLevelViewModel();
            model.SchoolId = user.School.Id;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> NewData(SchoolLevelViewModel data)
        {
            var user = await CurrentUser();
            if (Validate())
            {
                var result = await _lvl.AddSchoolLevel(data);
                if (result.Succeeded)
                {
                    return RedirectToAction("Success", new { message = "Creating new level " + data.Name + " succeeded!" });
                }
                else
                {
                    AddPageAlerts(PageAlertType.Warning, result.Error.Description);
                }
            }

            AddPageHeader("Levels");
            AddBreadcrumb("Levels", "/level");
            AddBreadcrumb("[New]", "");

            var model = new SchoolLevelViewModel();
            model.SchoolYearsSelections = _sy.GetSchoolYears(user.School.Id)
                                           .Select(o => new SelectListItem() { Text = o.Name, Value = o.Id.ToString() })
                                           .ToList();

            return View(model);
        }
        public async Task<IActionResult> EditData(Guid Id)
        {
            var user = await CurrentUser();
            var data = _lvl.GetSchoolLevel(Id);

            AddPageHeader("Levels");
            AddBreadcrumb("Levels", "/level/" + user.School.Id);
            AddBreadcrumb(data.Name, "");
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> EditData(SchoolLevelViewModel data)
        {
            var user = await CurrentUser();
            if (Validate())
            {
                var result = await _lvl.UpdateSchoolLevel(data);
                if (result.Succeeded)
                {
                    AddPageAlerts(PageAlertType.Success, "Updating level " + data.Name + " succeeded!");
                }
                else
                {
                    AddPageAlerts(PageAlertType.Warning, result.Error.Description);
                }
            }
            AddPageHeader("Levels");
            AddBreadcrumb("Levels", "/level/" + user.School.Id);
            AddBreadcrumb(data.Name, "");
            return View(data);
        }
        public IActionResult Success(string message)
        {
            AddPageAlerts(PageAlertType.Success, message);
            AddPageHeader("Levels");
            AddBreadcrumb("Levels", "/level");
            AddBreadcrumb("Success", "/level");
            return View();
        }

        public async Task<IActionResult> GetData()
        {
            try
            {
                var user = await CurrentUser();

                return this.BuildDataTableForm<SchoolLevelViewModel>(_lvl.GetSchoolLevels(user.School.Id));

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
