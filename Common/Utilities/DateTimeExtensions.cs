using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Common.Utilities
{
    public static class DateTimeExtensions
    {
        public static string TimeBeforeAfter(this DateTime value)
        {
            if (value != null && value > DateTime.MinValue)
            {
                long totalSecond = 0;
                long totalMin = 0;
                long totalHour = 0;
                long totalDay = 0;
                long totalMonth = 0;
                long totalYear = 0;

                int second = 0;
                int minute = 0;
                int hour = 0;
                int day = 0;
                int month = 0;
                int year = 0;

                DateTime now = DateTime.Now;
                bool before = value < now ? true : false;

                long nowTicks = now.Ticks;
                long valueTicks = value.Ticks;

                string result = "";

                if (before)
                {
                    var diff = new TimeSpan(nowTicks - valueTicks);
                    totalSecond = (long)diff.TotalSeconds;
                    totalMin = totalSecond / 60;
                    totalHour = totalMin / 60;
                    totalDay = totalHour / 24;
                    totalMonth = totalDay / 30;
                    totalYear = totalMonth / 12;

                    second = (int)totalSecond - ((int)totalMin * 60);
                    minute = (int)totalMin - ((int)totalHour * 60);
                    hour = (int)totalHour - ((int)totalDay * 24);
                    year = (int)totalDay / 365;
                    month = (int)totalMonth - ((int)totalYear * 12);
                    day = (int)totalDay - ((int)totalMonth * 30);

                    if (year > 0)
                    {
                        result = string.Format("{0}{1}", year > 0 ? string.Format("{0} سال ", year) : ""
                            , before ? "قبل" : "دیگر");
                    }
                    else if (month > 0)
                    {
                        result = string.Format("{0}{1}", month > 0 ? string.Format("{0} ماه ", month) : ""
                            , before ? "قبل" : "دیگر");
                    }
                    else if (day > 0)
                    {
                        result = string.Format("{0}{1}", day > 0 ? string.Format("{0} روز ", day) : ""
                            , before ? "قبل" : "دیگر");
                    }
                    else if (hour > 0)
                    {
                        result = string.Format("{0}{1}", hour > 0 ? string.Format("{0} ساعت ", hour) : ""
                            , before ? "قبل" : "دیگر");
                    }
                    else if (minute > 0)
                    {
                        result = string.Format("{0}{1}", minute > 0 ? string.Format("{0} دقیقه ", minute) : ""
                            , before ? "قبل" : "دیگر");
                    }
                    else if (second > 0)
                    {
                        result = string.Format("{0}{1}", second > 0 ? string.Format("{0} ثانیه ", second) : ""
                            , before ? "قبل" : "دیگر");
                    }

                    return result;
                }
                else
                {
                    var diff = new TimeSpan(valueTicks - nowTicks);
                    totalSecond = (long)diff.TotalSeconds;
                    totalMin = totalSecond / 60;
                    totalHour = totalMin / 60;
                    totalDay = totalHour / 24;
                    totalMonth = totalDay / 30;
                    totalYear = totalMonth / 12;

                    second = (int)totalSecond - ((int)totalMin * 60);
                    minute = (int)totalMin - ((int)totalHour * 60);
                    hour = (int)totalHour - ((int)totalDay * 24);
                    year = (int)totalDay / 365;
                    month = (int)totalMonth - ((int)totalYear * 12);
                    day = (int)totalDay - ((int)totalMonth * 30);

                    if (year > 0)
                    {
                        result = string.Format("{0}{1}", year > 0 ? string.Format("{0} سال ", year) : ""
                            , before ? "قبل" : "دیگر");
                    }
                    else if (month > 0)
                    {
                        result = string.Format("{0}{1}", month > 0 ? string.Format("{0} ماه ", month) : ""
                            , before ? "قبل" : "دیگر");
                    }
                    else if (day > 0)
                    {
                        result = string.Format("{0}{1}", day > 0 ? string.Format("{0} روز ", day) : ""
                            , before ? "قبل" : "دیگر");
                    }
                    else if (hour > 0)
                    {
                        result = string.Format("{0}{1}", hour > 0 ? string.Format("{0} ساعت ", hour) : ""
                            , before ? "قبل" : "دیگر");
                    }
                    else if (minute > 0)
                    {
                        result = string.Format("{0}{1}", minute > 0 ? string.Format("{0} دقیقه ", minute) : ""
                            , before ? "قبل" : "دیگر");
                    }
                    else if (second > 0)
                    {
                        result = string.Format("{0}{1}", second > 0 ? string.Format("{0} ثانیه ", second) : ""
                            , before ? "قبل" : "دیگر");
                    }

                    return result;
                }
            }

            return "";
        }

        public static string ToPersianDateFolderName(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            var pattern = $"{pc.GetYear(value)}_{pc.GetMonth(value)}_{pc.GetDayOfMonth(value)}";
            return pattern;
        }
    }
}
