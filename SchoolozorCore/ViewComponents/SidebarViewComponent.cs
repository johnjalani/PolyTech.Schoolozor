using Microsoft.AspNetCore.Mvc;
using Schoolozor.Model.ViewModel;
using Schoolozor.Services.Base.Common;
using System.Collections.Generic;
using System.Linq;

namespace SchoolozorCore.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        public SidebarViewComponent()
        {
        }

        public IViewComponentResult Invoke(string filter)
        {
            //you can do the access rights checking here by using session, user, and/or filter parameter
            var sidebars = new List<SidebarMenu>();

            sidebars.Add(ModuleHelper.AddModule("Home", "/", "fas fa-home"));
            sidebars.Add(ModuleHelper.AddModule("Students", "/student", "fas fa-user-graduate"));
            sidebars.Add(ModuleHelper.AddModule("Calendar", "/calendar", "fas fa-calendar-alt"));

            sidebars.Add(ModuleHelper.AddTree("Manage", "fas fa-user-cog"));
            sidebars.Last().TreeChild = new List<SidebarMenu>()
            {
                ModuleHelper.AddModule("School Years", "/schoolyear", "fas fa-book"),
                ModuleHelper.AddModule("Levels", "/level", "fas fa-angle-double-up"),
                ModuleHelper.AddModule("Sections", "/section", "fas fa-people-arrows"),
            };

            if (User.IsInRole("Admin"))
            {
                //sidebars.Add(ModuleHelper.AddModule(ModuleHelper.Module.SuperAdmin));
                //sidebars.Add(ModuleHelper.AddModule(ModuleHelper.Module.Role));
                //sidebars.Add(ModuleHelper.AddModule(ModuleHelper.Module.UserLogs));

                sidebars.Add(ModuleHelper.AddModule("Admin", "/superadmin", "fas fa-user-cog"));
                sidebars.Add(ModuleHelper.AddModule("Role", "/role", "fas fa-key"));
                sidebars.Add(ModuleHelper.AddModule("Logs", "/userlogs", "fas fa-clipboard-list"));
            }

            //sidebars.Add(ModuleHelper.AddTree("Admin"));
            //sidebars.Last().TreeChild = new List<SidebarMenu>()
            //{
            //    ModuleHelper.AddModule(ModuleHelper.Module.Login),
            //    ModuleHelper.AddModule(ModuleHelper.Module.Register, Tuple.Create(1, 1, 1)),
            //};

            return View(sidebars);
        }
    }
}
