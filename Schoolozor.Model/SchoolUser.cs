using Blazor.IndexedDB.WebAssembly;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using Schoolozor.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

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
        public string Token { get; set; }
    }
}
