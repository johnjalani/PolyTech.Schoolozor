using Blazor.IndexedDB.WebAssembly;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using Schoolozor.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolozor.Model
{
    public class SchoolIndexed : IndexedDb
    {
        public SchoolIndexed(IJSRuntime jSRuntime, string name, int version) : base(jSRuntime, name, version) { }
        public IndexedSet<SchoolUser> SchoolUser { get; set; }
        public IndexedSet<StudentAddress> StudentAddress { get; set; }
        public IndexedSet<StudentGuardian> StudentGuardian { get; set; }
        public IndexedSet<StudentProfile> StudentProfile { get; set; }
        public IndexedSet<StudentRecord> StudentRecord { get; set; }
        public IndexedSet<StudentAcademicActivity> StudentAcademicActivity { get; set; }
    }

    public class SchoolContext : IdentityDbContext<SchoolUser>
    {
        public SchoolContext()
        {

        }
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {

        }
        public DbSet<UserAudit> UserAuditEvents { get; set; }
        public DbSet<SchoolUser> SchoolUser { get; set; }
        public DbSet<StudentAddress> StudentAddress { get; set; }
        public DbSet<StudentGuardian> StudentGuardian { get; set; }
        public DbSet<StudentProfile> StudentProfile { get; set; }
        public DbSet<StudentRecord> StudentRecord { get; set; }
        public DbSet<StudentAcademicActivity> StudentAcademicActivity { get; set; }
    }
}
