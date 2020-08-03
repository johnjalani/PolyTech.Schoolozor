using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Schoolozor.Model.ViewModel;
using SchoolozorCore.Common;

namespace SchoolozorCore.Controllers
{
    public class HomeController : BaseController
    {
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
                    infoObj.sr = Convert.ToInt32(info[0].ToString());
                    infoObj.ordertracknumber = info[1].ToString();
                    infoObj.quantity = Convert.ToInt32(info[2].ToString());
                    infoObj.productname = info[3].ToString();
                    infoObj.specialoffer = info[4].ToString();
                    infoObj.unitprice = Convert.ToDouble(info[5].ToString());
                    infoObj.unitpricediscount = Convert.ToDouble(info[6].ToString());

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
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.sr).ToList()
                                                                                                 : data.OrderBy(p => p.sr).ToList();
                        break;

                    case "1":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ordertracknumber).ToList()
                                                                                                 : data.OrderBy(p => p.ordertracknumber).ToList();
                        break;

                    case "2":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.quantity).ToList()
                                                                                                 : data.OrderBy(p => p.quantity).ToList();
                        break;

                    case "3":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.productname).ToList()
                                                                                                 : data.OrderBy(p => p.productname).ToList();
                        break;

                    case "4":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.specialoffer).ToList()
                                                                                                   : data.OrderBy(p => p.specialoffer).ToList();
                        break;

                    case "5":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.unitprice).ToList()
                                                                                                 : data.OrderBy(p => p.unitprice).ToList();
                        break;

                    case "6":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.unitpricediscount).ToList()
                                                                                                 : data.OrderBy(p => p.unitpricediscount).ToList();
                        break;

                    default:

                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.sr).ToList()
                                                                                                 : data.OrderBy(p => p.sr).ToList();
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
