using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Common.Utilities
{
    public static class PersianCalendarExtention
    {
        private static PersianCalendar pc = new PersianCalendar();
        public static string ToPersian(this DateTime dt)
        {
            if (dt > DateTime.MinValue && dt.Year > 1300)
            {
                return $"{pc.GetYear(dt).ToString("0000")}/{pc.GetMonth(dt).ToString("00")}/{pc.GetDayOfMonth(dt).ToString("00")}";
            }

            return "";
        }

        public static string ToPersian(this DateTime? dt)
        {
            if (dt.HasValue && dt.Value > DateTime.MinValue && dt.Value.Year > 1300)
            {
                return $"{pc.GetYear(dt.Value).ToString("0000")}/{pc.GetMonth(dt.Value).ToString("00")}/{pc.GetDayOfMonth(dt.Value).ToString("00")}";
            }

            return "";
        }

    }
}
