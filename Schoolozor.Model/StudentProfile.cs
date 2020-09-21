using Schoolozor.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schoolozor.Model
{
    public enum Gender
    {
        Male,
        Female
    }
    public class StudentProfile : BaseContextFields
    {
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public virtual StudentAddress CurrentAddress { get; set; }
        public virtual StudentAddress PermanentAddress { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public virtual SchoolUser User { get; set; }
        public virtual SchoolProfile School { get; set; }
        public virtual IList<StudentRecord> Records { get; set; }
        public virtual IList<StudentGuardian> Guardians { get; set; }
        //public virtual StudentMasterList MasterList { get; set; }


    }
}
