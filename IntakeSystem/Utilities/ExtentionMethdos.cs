using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace IntakeSystem.Utilities
{
    public static class ExtentionMethdos
    {
        public static string ToShamsi(this DateTime date)
        {
            PersianCalendar calendar = new PersianCalendar();
            return
                $"{calendar.GetYear(date)}/{calendar.GetMonth(date)}/{calendar.GetDayOfMonth(date)} {date.Hour}:{date.Minute}:{date.Second}";
        }
    }
}