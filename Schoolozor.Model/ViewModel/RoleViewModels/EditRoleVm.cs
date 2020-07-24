using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Schoolozor.Model.ViewModel
{
    public class EditRoleVm
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<SchoolUser> Members { get; set; }
        public IEnumerable<SchoolUser> NonMembers { get; set; }
    }
}
