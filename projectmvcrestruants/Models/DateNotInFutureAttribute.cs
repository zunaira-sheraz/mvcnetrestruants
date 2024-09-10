using System;
using System.ComponentModel.DataAnnotations;

namespace projectmvcrestruants.Validation
{
    public class DateNotInFutureAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime? dob = value as DateTime?;

            if (dob.HasValue && dob.Value > DateTime.Now)
            {
                return false;
            }

            return true;
        }
    }
}
