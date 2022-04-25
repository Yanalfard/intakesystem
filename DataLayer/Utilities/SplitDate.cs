using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Utilities
{
    public static class SplitDate
    {
        public static List<DateTime> SplitDateTime(DateTime startDate, DateTime endDate)
        {
            var datesList = new List<DateTime>();

            datesList.Add(startDate);

            var currentDate = startDate;

            while (currentDate < endDate)
            {
                var newDate = currentDate.AddMinutes(15);
                datesList.Add(newDate);
                currentDate = newDate;
            }

            return datesList;
        }
    }
}
