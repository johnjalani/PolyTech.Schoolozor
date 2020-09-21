using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Schoolozor.Model.ViewModel.SchoolViewModels
{
    public class SchoolTeacherViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {MiddleName} {LastName}";
            }
        }
        [Required]
        public string Position { get; set; }
        public Guid UserId { get; set; }
        public List<NameValuePair> AssignedLevel { get; set; }
        public List<NameValuePair> AssignedSection { get; set; }
    }
}
