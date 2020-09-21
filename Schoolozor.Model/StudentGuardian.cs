using Schoolozor.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolozor.Model
{
    public enum GuardianType
    {
        Father,
        Mother,
        Uncle,
        Aunt,
        Grandfather,
        Grandmother,
        Guardian
    }

    public class StudentGuardian : BaseContextFields
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName
        {
            get
            {

                return $"{FirstName.ToProperCase()} {(!string.IsNullOrEmpty(MiddleName) ? MiddleName.ToProperCase().Substring(0, 1) : string.Empty)} {LastName.ToProperCase()}";
            }
        }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public GuardianType type { get; set; }
        public virtual SchoolUser User { get; set; }
    }
}
