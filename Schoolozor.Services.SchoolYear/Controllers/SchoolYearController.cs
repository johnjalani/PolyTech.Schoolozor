using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Schoolozor.Model;
using Schoolozor.Model.ViewModel.SchoolViewModels;
using Schoolozor.Services.Base.Common;
using Schoolozor.Services.Base.Controllers;
using Schoolozor.Services.SchoolYear.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolozor.Services.SchoolYear.Controllers
{
    [Authorize(Roles = "Admin,Staff,Teacher,Parent")]
    public class SchoolYearController : BaseController
    {
        private readonly SchoolYearServices _sy;
        public SchoolYearController(SchoolYearServices sy, UserManager<SchoolUser> userManager) : base(userManager)
        {
            _sy = sy;
        }

        public IActionResult Index()
        {
            AddPageHeader("School Years");
            return View();
        }
        [Route("schoolyear/new")]
        public IActionResult NewData()
        {
            AddPageHeader("School Years");
            AddBreadcrumb("School Years", "/schoolyear");
            AddBreadcrumb("[New]", "");
            return View(new SchoolYearViewModel());
        }
        [HttpPost]
        [Route("schoolyear/new")]
        public async Task<IActionResult> NewData(SchoolYearViewModel data)
        {

            if (Validate())
            {
                var user = await CurrentUser();

                //check for overlapping dates from existing school years
                var sys = _sy.GetSchoolYears(user.School.Id);
                foreach (var sy in sys)
                {
                    if (sy.Start.Value <= data.Start.Value && data.Start.Value <= sy.End.Value)
                    {
                        AddPageAlerts(PageAlertType.Warning, "Your date range is overlapping to " + sy.Name + "!");
                        return View();
                    }
                    else if (sy.Start.Value <= data.End.Value && data.End.Value <= sy.End.Value)
                    {
                        AddPageAlerts(PageAlertType.Warning, "Your date range is overlapping to " + sy.Name + "!");
                        return View();
                    }

                }


                var result = await _sy.AddSchoolYear(data, user.School);
                if (result.Succeeded)
                {
                    return RedirectToAction("success", new { message = "Creating new school year " + data.Name + " succeeded!" });
                }
                else
                {
                    AddPageAlerts(PageAlertType.Warning, result.Error.Description);
                }
            }

            return View();
        }

        [Route("schoolyear/{Id}")]
        public IActionResult EditData(Guid Id)
        {
            var data = _sy.GetSchoolYear(Id);

            AddPageHeader("School Years");
            AddBreadcrumb("School Years", "/schoolyear");
            AddBreadcrumb(data.Name, "");
            return View(new SchoolYearViewModel()
            {
                Id = data.Id,
                Name = data.Name,
                Start = data.Start,
                End = data.End,
                SchoolId = data.SchoolId
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditData(SchoolYearViewModel data)
        {

            if (Validate())
            {
                var user = await CurrentUser();

                //check for overlapping dates from existing school years
                var sys = _sy.GetSchoolYears(user.School.Id);
                foreach (var sy in sys.Where(o => o.Id != data.Id))
                {
                    if (sy.Start.Value <= data.Start.Value && data.Start.Value <= sy.End.Value)
                    {
                        AddPageAlerts(PageAlertType.Warning, "Your date range is overlapping to " + sy.Name + "!");
                        return View();
                    }
                    else if (sy.Start.Value <= data.End.Value && data.End.Value <= sy.End.Value)
                    {
                        AddPageAlerts(PageAlertType.Warning, "Your date range is overlapping to " + sy.Name + "!");
                        return View();
                    }
                    if (sy.Name.ToLower().Trim() == data.Name.ToLower().Trim())
                    {
                        AddPageAlerts(PageAlertType.Warning, "The name you use has already exist!");
                        return View();
                    }

                }

                var result = await _sy.UpdateSchoolYear(data, user.School);
                if (result.Succeeded)
                {
                    return RedirectToAction("success", new { message = "Updating school year " + data.Name + " succeeded!" });
                }
                else
                {
                    AddPageAlerts(PageAlertType.Warning, result.Error.Description);
                }
            }
            AddPageHeader("School Years");
            AddBreadcrumb("School Years", "/schoolyear");
            AddBreadcrumb(data.Name, "");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteData(SchoolYearViewModel data)
        {
            var user = await CurrentUser();
            var result = await _sy.DeleteSchoolYear(data);
            if (result.Succeeded)
            {
                return RedirectToAction("success", new { message = "Deleting school year " + data.Name + " succeeded!" });
            }
            else
            {
                AddPageHeader("School Years");
                AddBreadcrumb("School Years", "/schoolyear");
                AddBreadcrumb(data.Name, "");
                AddPageAlerts(PageAlertType.Warning, result.Error.Description);
                return RedirectToAction("editdata", new { data.Id });
            }
        }

        [Route("schoolyear/success")]
        public IActionResult Success(string message)
        {
            AddPageAlerts(PageAlertType.Success, message);
            AddPageHeader("School Years");
            AddBreadcrumb("School Years", "/schoolyear");
            return View();
        }

        public async Task<IActionResult> GetData()
        {
            try
            {
                var user = await CurrentUser();
                return this.BuildDataTableForm<SchoolYearViewModel>(_sy.GetSchoolYears(user.School.Id));
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
