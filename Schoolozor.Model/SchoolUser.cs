using Microsoft.AspNetCore.Identity;
using Schoolozor.Shared;
using System;

namespace Schoolozor.Model
{
    public enum UserType
    {
        Admin,
        Clerk,
        Teacher,
        Parent,
        Student
    }
    public class SchoolUser : IdentityUser
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
        public UserType Type { get; set; }
        public DateTime InsertedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public virtual SchoolProfile School { get; set; }
    }
}
