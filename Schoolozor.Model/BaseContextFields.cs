using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolozor.Model
{
    public class BaseContextFields
    {
        public Guid Id { get; set; }
        public DateTime InsertedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public DateTime? DeletedDateTime { get; set; }
    }
}
