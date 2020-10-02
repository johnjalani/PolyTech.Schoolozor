using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Schoolozor.Model.ViewModel;
using Schoolozor.Services.Base.Common;
using Schoolozor.Model;
using Microsoft.AspNetCore.Identity;

namespace Schoolozor.Services.Base.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(UserManager<SchoolUser> userManager) : base(userManager)
        {

        }
        [HelpDefinition]
        public IActionResult Index()
        {
            AddPageHeader("Dashboard", "");
            AddBreadcrumb("Student", "");
            AddBreadcrumb("Jose Rizal", "");
            AddPageAlerts(PageAlertType.Info, "You can also use our school activity templates");
            AddPageAlerts(PageAlertType.Error, "You can also use our school activity templates");
            AddPageAlerts(PageAlertType.Warning, "You can also use our school activity templates");
            AddPageAlerts(PageAlertType.Success, "You can also use our school activity templates");
            return View();
        }

        [HttpPost]
        public IActionResult Index(object model)
        {
            AddPageAlerts(PageAlertType.Info, "you may view the summary <a href='#'>here</a>");
            return View("Index");
        }

        [HelpDefinition]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            AddBreadcrumb("About", "/Account/About");

            return View();
        }

        [HelpDefinition("helpdefault")]
        public IActionResult Contact()
        {
            AddBreadcrumb("Register", "/Account/Register");
            AddBreadcrumb("Contact", "/Account/Contact");
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        #region Get data method.

        /// <summary>
        /// GET: /Home/GetData
        /// </summary>
        /// <returns>Return data</returns>
        public ActionResult GetData()
        {
            try
            {

                return this.BuildDataTableForm<SalesOrderDetail>(this.LoadData());

            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
                return null;
            }
        }

        #endregion

        #region Helpers

        #region Load Data

        /// <summary>
        /// Load data method.
        /// </summary>
        /// <returns>Returns - Data</returns>
        private List<SalesOrderDetail> LoadData()
        {
            // Initialization.
            List<SalesOrderDetail> lst = new List<SalesOrderDetail>();

            try
            {
                // Initialization.
                string line = string.Empty;
                //string srcFilePath = "content/files/SalesOrderDetail.txt";
                //var rootPath = Path.GetDirectoryName(AppContext.BaseDirectory);
                //var fullPath = Path.Combine(rootPath, srcFilePath);
                //string filePath = new Uri(fullPath).LocalPath;
                StreamReader sr = new StreamReader(new FileStream(@"wwwroot\files\SalesOrderDetail.txt", FileMode.Open, FileAccess.Read));

                // Read file.
                while ((line = sr.ReadLine()) != null)
                {
                    // Initialization.
                    SalesOrderDetail infoObj = new SalesOrderDetail();
                    string[] info = line.Split(',');

                    // Setting.
                    infoObj.Sr = Convert.ToInt32(info[0].ToString());
                    infoObj.OrderTrackNumber = info[1].ToString();
                    infoObj.Quantity = Convert.ToInt32(info[2].ToString());
                    infoObj.ProductName = info[3].ToString();
                    infoObj.SpecialOffer = info[4].ToString();
                    infoObj.UnitPrice = Convert.ToDouble(info[5].ToString());
                    infoObj.UnitPriceDiscount = Convert.ToDouble(info[6].ToString());

                    // Adding.
                    lst.Add(infoObj);
                }

                // Closing.
                sr.Dispose();
            }
            catch (Exception ex)
            {
                // info.
                Console.Write(ex);
            }

            // info.
            return lst;
        }

        #endregion

        #region Sort by column with order method

        /// <summary>
        /// Sort by column with order method.
        /// </summary>
        /// <param name="order">Order parameter</param>
        /// <param name="orderDir">Order direction parameter</param>
        /// <param name="data">Data parameter</param>
        /// <returns>Returns - Data</returns>
        private List<SalesOrderDetail> SortByColumnWithOrder(string order, string orderDir, List<SalesOrderDetail> data)
        {
            // Initialization.
            List<SalesOrderDetail> lst = new List<SalesOrderDetail>();

            try
            {
                // Sorting
                switch (order)
                {
                    case "0":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Sr).ToList()
                                                                                                 : data.OrderBy(p => p.Sr).ToList();
                        break;

                    case "1":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.OrderTrackNumber).ToList()
                                                                                                 : data.OrderBy(p => p.OrderTrackNumber).ToList();
                        break;

                    case "2":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Quantity).ToList()
                                                                                                 : data.OrderBy(p => p.Quantity).ToList();
                        break;

                    case "3":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ProductName).ToList()
                                                                                                 : data.OrderBy(p => p.ProductName).ToList();
                        break;

                    case "4":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.SpecialOffer).ToList()
                                                                                                   : data.OrderBy(p => p.SpecialOffer).ToList();
                        break;

                    case "5":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.UnitPrice).ToList()
                                                                                                 : data.OrderBy(p => p.UnitPrice).ToList();
                        break;

                    case "6":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.UnitPriceDiscount).ToList()
                                                                                                 : data.OrderBy(p => p.UnitPriceDiscount).ToList();
                        break;

                    default:

                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Sr).ToList()
                                                                                                 : data.OrderBy(p => p.Sr).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                // info.
                Console.Write(ex);
            }

            // info.
            return lst;
        }

        #endregion

        #endregion
    }
}
