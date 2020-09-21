using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Schoolozor.Model;
using Schoolozor.Model.ViewModel;
using Schoolozor.Model.ViewModel.SchoolViewModels;
using Schoolozor.Services.Base.Common;
using Schoolozor.Services.Base.Controllers;
using Schoolozor.Services.Level.Services;
using Schoolozor.Services.SchoolYear.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        [Route("level/{schoolYearId}")]
        public async Task<IActionResult> Index(Guid schoolYearId)
        {
            AddPageHeader("Levels");
            var user = await CurrentUser();
            var schoolYear = _sy.GetSchoolYear(schoolYearId);
            var schoolYears = _sy.GetSchoolYears(user.School.Id);
            if (schoolYear == null)
            {
                AddPageAlerts(PageAlertType.Warning, "Please add school years!");
            }
            else
            {
                AddPageAlerts(PageAlertType.Info, "Copy from the <a href='#'>previous school years?</a>");
            }

            var syList = new List<NameValuePair>();
            foreach (var item in schoolYears)
            {
                syList.Add(new NameValuePair { Name = item.Name, Value = item.Id });
            }
            return View(new Tuple<Guid, List<NameValuePair>>(schoolYearId, syList));
        }
        public async Task<IActionResult> NewData()
        {
            AddPageHeader("Levels");
            AddBreadcrumb("Levels", "/level");
            AddBreadcrumb("[New]", "");

            var user = await CurrentUser();
            var model = new SchoolLevelViewModel();
            model.SchoolYearsSelections = _sy.GetSchoolYears(user.School.Id)
                                           .Select(o => new SelectListItem() { Text = o.Name, Value = o.Id.ToString() })
                                           .ToList();

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
                var schoolYear = _sy.GetCurrentSchoolYear(user.School.Id);
                if (schoolYear != null)
                {
                    return this.BuildDataTableForm<SchoolLevelViewModel>(_lvl.GetSchoolLevels(schoolYear.Id));
                }
                else
                {
                    return Json(new List<SchoolLevelViewModel>());
                }
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
                return null;
            }
        }
        [Route("level/getdata/{schoolYearId}")]
        public IActionResult GetData(Guid schoolYearId)
        {
            try
            {
                return this.BuildDataTableForm<SchoolLevelViewModel>(_lvl.GetSchoolLevels(schoolYearId));
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
