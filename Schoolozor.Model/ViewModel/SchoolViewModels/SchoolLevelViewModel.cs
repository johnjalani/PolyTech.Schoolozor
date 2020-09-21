using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Schoolozor.Model.ViewModel.SchoolViewModels
{
    public class SchoolLevelViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Guid SchoolId { get; set; }
        [Required]
        [Display(Name = "School Year")]
        public Guid SchoolYearId { get; set; }
        [Display(Name = "School Year")]
        public string SchoolYear { get; set; }
        public List<SelectListItem> SchoolYearsSelections { get; set; }
    }
}
