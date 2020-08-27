using Schoolozor.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Schoolozor.Model.ViewModel.SchoolViewModels
{
    public class StudentViewModel
    {
        public Guid UserId { get; set; }
        public Guid SchoolId { get; set; }

        public Guid Id { get; set; }
        [Display(Name ="Student ID")]
        public string StudentId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Display(Name = "Student Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName.ToProperCase()} {(!string.IsNullOrEmpty(MiddleName) ? MiddleName.ToProperCase().Substring(0, 1) : string.Empty)} {LastName.ToProperCase()}";
            }
        }
        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }
        [Display(Name = "Age")]
        public double Age
        {
            get
            {
                return DateTime.Now.Year - DOB.Year;
            }
        }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public virtual StudentAddress CurrentAddress { get; set; }
        public virtual StudentAddress PermanentAddress { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }     
    }
}
