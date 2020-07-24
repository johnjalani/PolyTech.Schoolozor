using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;

namespace Schoolozor.Shared
{
    public static class String
    {
        public static string ToProper(this string value)
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
    }
}
