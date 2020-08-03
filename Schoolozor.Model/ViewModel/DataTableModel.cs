using Schoolozor.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schoolozor.Model.ViewModel
{
    public class DataTableModel
    {
        public string DataUrl { get; set; }
        public List<DataTableHeaders> Headers { get; set; }
    }
    public class DataTableHeaders
    {
        public int Id { get; set; }
        public string Label { get; set; }
        private string _Field;

        public string Field
        {
            get { return _Field.ToCamelCase(); }
            set { _Field = value; }
        }

        public int Sort { get; set; }

    }
}
