using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Schoolozor.Model
{
    public class SchoolProfile : BaseDate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual IList<SchoolUser> Users { get; set; }
        public virtual IList<SchoolYear> SchoolYears { get; set; }
    }
}
