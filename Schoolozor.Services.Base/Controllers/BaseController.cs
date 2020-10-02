using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using Schoolozor.Model.ViewModel;
using Microsoft.AspNetCore.Identity;
using Schoolozor.Model;

namespace Schoolozor.Services.Base.Controllers
{
    public class BaseController : Controller
    {
        private UserManager<SchoolUser> _userManager;
        public BaseController(UserManager<SchoolUser> userManager)
        {
            _userManager = userManager;
        }

        public void AddBreadcrumb(string displayName, string urlPath)
        {
            List<Message> messages;

            if (ViewBag.Breadcrumb == null)
            {
                messages = new List<Message>();
            }
            else
            {
                messages = ViewBag.Breadcrumb as List<Message>;
            }

            messages.Add(new Message { DisplayName = displayName, URLPath = urlPath });
            ViewBag.Breadcrumb = messages;
        }

        public void AddPageHeader(string pageHeader = "", string pageDescription = "")
        {
            ViewBag.PageHeader = Tuple.Create(pageHeader, pageDescription);
        }

        public enum PageAlertType
        {
            Error,
            Info,
            Warning,
            Success
        }

        public void AddPageAlerts(PageAlertType pageAlertType, string description)
        {
            List<Message> messages;

            if (ViewBag.PageAlerts == null)
            {
                messages = new List<Message>();
            }
            else
            {
                messages = ViewBag.PageAlerts as List<Message>;
            }

            messages.Add(new Message { Type = pageAlertType.ToString().ToLower(), ShortDesc = description });
            ViewBag.PageAlerts = messages;
        }
        public async Task<SchoolUser> CurrentUser()
        { 
            var user = await _userManager.GetUserAsync(User);
            return user;
        }

        public bool Validate()
        {
            if (ModelState.IsValid)
            {
                return true;
            }
            else {
                var msg = "";
                foreach (var i in ModelState.Values)
                {
                    if (i.Errors.Count > 0)
                    {
                        foreach (var e in i.Errors)
                        {
                            msg += "<br />" + e.ErrorMessage; 
                        }
                    }
                }

                AddPageAlerts(PageAlertType.Warning, msg);
                return false;
            }
        }
    }
}