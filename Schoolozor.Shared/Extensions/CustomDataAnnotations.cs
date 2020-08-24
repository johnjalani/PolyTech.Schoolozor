using System;
using System.ComponentModel.DataAnnotations;

namespace Schoolozor.Shared.Extensions
{
    public class CustomDataAnnotations
    {
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
        public class NotPastDateAttribute : ValidationAttribute
        {
            public override string FormatErrorMessage(string name)
            {
                return "The " + name + " field must not be less than the date today!";
            }
            public override bool IsValid(object value)
            {
                var dt = (DateTime?)value;
                if (!dt.HasValue)
                {
                    return true;
                }
                if (dt >= DateTime.Now)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Enter property End Date
        /// </summary>
        public class CheckDateNotFlipAttribute : ValidationAttribute
        {
            public string EndDate { get; }
            public CheckDateNotFlipAttribute(string EndDate)
            {
                this.EndDate = EndDate;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var dt = (DateTime?)value;
                if (!dt.HasValue)
                {
                    return ValidationResult.Success;
                }

                var prop = validationContext.ObjectType.GetProperty(this.EndDate);

                var endDt = (DateTime?)prop.GetValue(validationContext.ObjectInstance);
                if (!endDt.HasValue)
                {
                    return ValidationResult.Success;
                }

                if (dt < endDt)
                {
                    return ValidationResult.Success;
                }

                if (prop.GetCustomAttributes(typeof(DisplayAttribute), true).Length > 0)
                {
                    var label = prop.GetCustomAttributes(typeof(DisplayAttribute), true);
                    return new ValidationResult("The " + ((DisplayAttribute)label[0]).Name + " field should not be less than the " + validationContext.DisplayName + " field!");
                }
                else {
                    return new ValidationResult("The " + this.EndDate + " field should not be less than the " + validationContext.DisplayName + " field!");
                }

                
            }
        }
    }
}
