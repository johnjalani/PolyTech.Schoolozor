using System;
using System.Linq;

namespace Schoolozor.Shared
{
    public static class String
    {
        public static string ToProperCase(this string value)
        {
            if (value != null)
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
            else {
                return null;
            }
        }
        /// <summary>
        /// For Models only, it only makes the first character to lowercase
        /// ApplicationId => applicationId
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string value)
        {
            if (value != null)
            {
                value = value.Substring(0, 1).ToLower() + value.Substring(1, value.Length - 1);
                return value;
            }
            else {
                return null;
            }
        }

        public static string GenerateRandom(int length, bool hasLetters, bool hasNumbers, bool hasLower, bool hasUpper)
        {
            var chars = "";
            if (hasLetters)
            {
                if (hasLower && hasUpper)
                {
                    chars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    chars += "abcdefghijklmnopqrstuvwxyz";
                }
                else if (hasUpper)
                {
                    chars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                }
                else if (hasLower)
                {
                    chars += "abcdefghijklmnopqrstuvwxyz";
                }
            }
            if (hasNumbers)
            {
                chars += "0123456789";
            }
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new System.String(stringChars);

            return finalString;

        }
    }
}
