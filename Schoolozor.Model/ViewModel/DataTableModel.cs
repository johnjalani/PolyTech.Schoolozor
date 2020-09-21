using Microsoft.AspNetCore.Identity;
using Schoolozor.Shared;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Schoolozor.Model.ViewModel
{
    public enum fieldType
    { 
        DATE,
        LINK,
        ENUM,
        CLASS,
        LIST,
        DICT
    }
    public class DataTableModel
    {
        public string DataUrl { get; set; }
        public List<DataTableHeaders> Headers { get; set; }
        public List<DataTableControls> Controls { get; set; }
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
        public string Api { get; set; }
        public fieldType? Type { get; set; }
        public string CssClass { get; set; }
    }

    public class DataTableControls
    {
        public string Label { get; set; }
        public string Type { get; set; }

    }
    public class DataTableButton : DataTableControls
    {
        public DataTableButton()
        {
            this.Type = "Button";
        }
        public string Href { get; set; }
    }
    public class DataTableList : DataTableControls
    {
        public DataTableList()
        {
            this.Type = "List";
        }
        public List<NameValuePair> Items { get; set; }
    }
}
