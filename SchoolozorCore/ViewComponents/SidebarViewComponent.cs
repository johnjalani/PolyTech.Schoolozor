using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SchoolozorCore.Common;
using System.Security.Claims;
using System;
using Schoolozor.Model.ViewModel;

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

            if (User.IsInRole("SuperAdmins"))
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
