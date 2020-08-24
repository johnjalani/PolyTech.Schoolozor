using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Schoolozor.Shared;

namespace Schoolozor.Services.Base.Common
{
    public static class GlobalHelper
    {
        public static JsonResult BuildDataTableForm<T>(this Microsoft.AspNetCore.Mvc.Controller controller, List<T> data)
        {

            // Initialization.
            string search = controller.Request.Form["search[value]"][0];
            string draw = controller.Request.Form["draw"][0];
            string order = controller.Request.Form["order[0][column]"][0];
            string orderDir = controller.Request.Form["order[0][dir]"][0];
            int startRec = Convert.ToInt32(controller.Request.Form["start"][0]);
            int pageSize = Convert.ToInt32(controller.Request.Form["length"][0]);


            // Total record count.
            int totalRecords = data.Count;

            // Verification.
            if (!string.IsNullOrEmpty(search) &&
                !string.IsNullOrWhiteSpace(search))
            {
                // Apply search
                var tempData = new List<T>();
                foreach (var item in typeof(T).GetProperties())
                {
                    tempData.AddRange(data.Where(m => { return m.GetType().GetProperty(item.Name).GetValue(m, null).ToString().ToLower().Contains(search.ToLower()); }));
                }
                data = tempData.Distinct().ToList();
            }

            // Sorting.
            data = SortByColumnWithOrder(order, orderDir, data);

            // Filter record count.
            int recFilter = data.Count;

            // Apply pagination.
            data = data.Skip(startRec).Take(pageSize).ToList();

            var finalData = new List<Dictionary<string, object>>();
            foreach (var item in data)
            {
                //TODO: When you add a new data formatting here, make sure to add also in dataTable.js Ln 24
                var expObj = new Dictionary<string, object>();
                foreach (var p in item.GetType().GetProperties())
                {
                    expObj.Add(p.Name.ToCamelCase(), p.GetValue(item));
                    if (p.PropertyType.BaseType == typeof(Enum))
                    {
                        expObj.Add(p.Name.ToCamelCase() + "Desc", p.GetValue(item).ToString());
                    }
                    else if (p.PropertyType == typeof(DateTimeOffset))
                    {
                        expObj.Add(p.Name.ToCamelCase() + "Desc", DateTime.Parse(p.GetValue(item).ToString()).ToString("MMM d, yyyy HH:mm:ss"));
                    }
                }
                finalData.Add(expObj);
            }

            // Loading drop down lists.
            var result = controller.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = recFilter, data = finalData });
            return result;
        }

        #region Sort by column with order method

        /// <summary>
        /// Sort by column with order method.
        /// </summary>
        /// <param name="order">Order parameter</param>
        /// <param name="orderDir">Order direction parameter</param>
        /// <param name="data">Data parameter</param>
        /// <returns>Returns - Data</returns>
        private static List<T> SortByColumnWithOrder<T>(string order, string orderDir, List<T> data)
        {
            // Sorting.
            var lst = new List<T>();
            lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(o => typeof(T).GetProperties()[int.Parse(order)].GetValue(o, null)).ToList()
                                                                                     : data.OrderBy(o => typeof(T).GetProperties()[int.Parse(order)].GetValue(o, null)).ToList();


            return lst;
        }

        #endregion
    }
}
