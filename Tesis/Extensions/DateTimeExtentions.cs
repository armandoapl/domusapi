using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tesis.Extensions
{
    public static class DateTimeExtentions
    {
        public static int CalculateAge(this DateTime dateOBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOBirth.Year;
            if (dateOBirth.Date > today.AddYears(-age)) 
                age--;
            return age;
        }
    }
}
