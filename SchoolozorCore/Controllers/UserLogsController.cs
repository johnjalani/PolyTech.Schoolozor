using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Schoolozor.Model;
using Schoolozor.Model.ViewModel;
using SchoolozorCore.Common;

namespace SchoolozorCore.Controllers
{
    [Authorize(Roles = "SuperAdmins")]
    public class UserLogsController : BaseController
    {
        private readonly SchoolContext _context;

        public UserLogsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public ViewResult Index()
        {
            AddPageHeader("Logs", "");
            return View();
        }

        public ActionResult GetData()
        {
            return this.BuildDataTableForm<UserAudit>(_context.UserAuditEvents.ToList());
        }
    }
}
