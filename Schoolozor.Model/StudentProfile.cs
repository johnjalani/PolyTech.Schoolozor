using Schoolozor.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolozor.Model
{
    public enum Gender
    {
        Male,
        Female
    }
    public class StudentProfile
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName.ToProper()} {(!string.IsNullOrEmpty(MiddleName) ? MiddleName.ToProper().Substring(0, 1) : string.Empty)} {LastName.ToProper()}";
            }
        }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public StudentAddress CurrentAddress { get; set; }
        public StudentAddress PermanentAddress { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public SchoolUser User { get; set; }
        public List<StudentGuardian> Guardians { get; set; }
        public List<StudentRecord> Records { get; set; }
        public DateTime InsertDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }

    }
}
