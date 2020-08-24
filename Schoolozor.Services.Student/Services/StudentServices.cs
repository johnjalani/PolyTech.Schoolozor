using Schoolozor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Schoolozor.Services.Student.Services
{
    public class StudentServices
    {
        private readonly IDataManager<StudentProfile> _manager;
        public StudentServices(IDataManager<StudentProfile> manager)
        {
            _manager = manager;
        }
        public List<StudentProfile> GetStudents()
        {
            return _manager.GetAll().ToList();
        }
        public List<StudentProfile> GetStudents(Guid schoolId)
        {

            return _manager.GetList(o=>o.School.Id == schoolId && o.DeletedDateTime == null).ToList();
        }
    }
}
