using Microsoft.AspNetCore.Mvc;
using Schoolozor.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolozorCore.ViewComponents
{
    public class ModalActionViewComponent : ViewComponent
    {
        public ModalActionViewComponent()
        {

        }

        public IViewComponentResult Invoke(ModalActionModel model)
        {
            return View(model);
        }
    }
}
