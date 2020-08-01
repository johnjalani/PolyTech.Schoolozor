using Microsoft.AspNetCore.Mvc;
using Schoolozor.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolozorCore.ViewComponents
{
    public class DataTableViewComponent : ViewComponent
    {
        public DataTableViewComponent()
        {

        }
        public IViewComponentResult Invoke(DataTableModel model)
        {
            return View(model);
        }
    }
}
