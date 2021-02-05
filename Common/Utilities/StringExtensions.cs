using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Utilities
{
    public static class StringExtensions
    {
        public static string QoutedPrintableToString(this string value, string charset)
        {
            Encoding enc;

            try
            {
                enc = Encoding.GetEncoding(charset);
            }
            catch
            {
                enc = new UTF8Encoding();
            }

            var occurences = new Regex(@"(=[0-9A-Z]{2}){1,}", RegexOptions.Multiline);
            var matches = occurences.Matches(value);

            foreach (Match match in matches)
            {
                try
                {
                    byte[] b = new byte[match.Groups[0].Value.Length / 3];
                    for (int i = 0; i < match.Groups[0].Value.Length / 3; i++)
                    {
                        b[i] = byte.Parse(match.Groups[0].Value.Substring(i * 3 + 1, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                    }
                    char[] hexChar = enc.GetChars(b);
                    value = value.Replace(match.Groups[0].Value, new String(hexChar));
                }
                catch
                {; }
            }
            value = value.Replace("?=", "");

            return value;
        }
        public static bool HasValue(this string value, bool ignoreWhiteSpace = true)
        {
            return ignoreWhiteSpace ? !string.IsNullOrWhiteSpace(value) : !string.IsNullOrEmpty(value);
        }
        public static string Encrypt(this string text)
        {
            return SecurityHelper.Encrypt(text);
        }
        public static string Decrypt(this string text)
        {
            return SecurityHelper.Decrypt(text);
        }
        public static string LongTextEncrypt(this string text)
        {
            return SecurityHelper.LongTextEncrypt(text);
        }
        public static string LongTextDecrypt(this string text)
        {
            return SecurityHelper.LongTextDecrypt(text);
        }
        public static int ToInt(this string value)
        {
            return Convert.ToInt32(value);
        }
        public static decimal ToDecimal(this string value)
        {
            return Convert.ToDecimal(value);
        }
        public static string ToNumeric(this int value)
        {
            return value.ToString("N0"); //"123,456"
        }

        public static string ToNumeric(this double value)
        {
            return value.ToString("N0"); //"123,456"
        }
        public static string ToNumeric(this decimal value)
        {
            return value.ToString("N0");
        }
        public static string ToCurrency(this int value)
        {
            //fa-IR => current culture currency symbol => ریال
            //123456 => "123,123ریال"
            return value.ToString("C0");
        }
        public static string ToCurrency(this decimal value)
        {
            return value.ToString("C0");
        }
        public static string En2Fa(this string str)
        {
            return str.Replace("0", "۰")
                .Replace("1", "۱")
                .Replace("2", "۲")
                .Replace("3", "۳")
                .Replace("4", "۴")
                .Replace("5", "۵")
                .Replace("6", "۶")
                .Replace("7", "۷")
                .Replace("8", "۸")
                .Replace("9", "۹");
        }
        public static string Fa2En(this string str)
        {
            return str.Replace("۰", "0")
                .Replace("۱", "1")
                .Replace("۲", "2")
                .Replace("۳", "3")
                .Replace("۴", "4")
                .Replace("۵", "5")
                .Replace("۶", "6")
                .Replace("۷", "7")
                .Replace("۸", "8")
                .Replace("۹", "9")
                //iphone numeric
                .Replace("٠", "0")
                .Replace("١", "1")
                .Replace("٢", "2")
                .Replace("٣", "3")
                .Replace("٤", "4")
                .Replace("٥", "5")
                .Replace("٦", "6")
                .Replace("٧", "7")
                .Replace("٨", "8")
                .Replace("٩", "9")
                .Replace("  ", " ");
        }
        public static string FixPersianChars(this string str)
        {
            try
            {
                return str.Replace("ﮎ", "ک")
                    .Replace("ﮏ", "ک")
                    .Replace("ﮐ", "ک")
                    .Replace("ﮑ", "ک")
                    .Replace("ك", "ک")
                    .Replace("ي", "ی")
                    .Replace(" ", " ")
                    .Replace("‌", " ")
                    .Replace("ھ", "ه")
                    .Replace("۰", "0")
                    .Replace("۱", "1")
                    .Replace("۲", "2")
                    .Replace("۳", "3")
                    .Replace("۴", "4")
                    .Replace("۵", "5")
                    .Replace("۶", "6")
                    .Replace("۷", "7")
                    .Replace("۸", "8")
                    .Replace("۹", "9")
                    //iphone numeric
                    .Replace("٠", "0")
                    .Replace("١", "1")
                    .Replace("٢", "2")
                    .Replace("٣", "3")
                    .Replace("٤", "4")
                    .Replace("٥", "5")
                    .Replace("٦", "6")
                    .Replace("٧", "7")
                    .Replace("٨", "8")
                    .Replace("٩", "9")
                    .Trim();//.Replace("ئ", "ی");
            }
            catch (Exception Ex)
            {

            }

            return str;
        }
        public static string CleanString(this string str)
        {
            return str.Trim().FixPersianChars().Fa2En().NullIfEmpty();
        }
        public static string NullIfEmpty(this string str)
        {
            return str?.Length == 0 ? null : str;
        }
        public static string ClearSpecialsNumbersAndSpaces(this string value)
        {
            return value.Trim()
                .Replace("%", "")
                .Replace("0", "")
                .Replace("1", "")
                .Replace("2", "")
                .Replace("3", "")
                .Replace("4", "")
                .Replace("5", "")
                .Replace("6", "")
                .Replace("7", "")
                .Replace("8", "")
                .Replace("9", "");
        }
        public static string LengthSubstring(this string value, int length)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (value.Length > length)
                {
                    return value.Substring(0, length);
                }

                return value;
            }

            return "";
        }
        public static TDto FromJson<TDto>(this string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TDto>(json);
        }
        public static string ToJson(this object json)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(json);
        }
        public static IEnumerable<string> ToListOfString(this string str)
        {
            try
            {
                return str.Split(new char[] { ',' }).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string ToListOfString(this IEnumerable<string> strs)
        {
            try
            {
                return string.Join(',', strs);
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
