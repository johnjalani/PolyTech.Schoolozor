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
        public string Field { get; set; }
        public int Sort { get; set; }

    }
}
