using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Schoolozor.Shared
{
    public static class String
    {
        public static string ToProperCase(this string value)
        {
            var values = value.Split(' ');
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].ToLower();
                values[i] = values[i].Substring(0, 1).ToUpper() + values[i].Substring(1, values[i].Length - 1);
            }

            value = values.Aggregate((first, next) => first + " " + next);
            return value;
        }
        /// <summary>
        /// For Models only, it only makes the first character to lowercase
        /// ApplicationId => applicationId
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string value)
        {
            value = value.Substring(0, 1).ToLower() + value.Substring(1, value.Length - 1);
            return value;
        }
    }
}
