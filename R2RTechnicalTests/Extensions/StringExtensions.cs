using System;
using System.Globalization;

namespace R2RTechnicalTests.Extensions
{
    public static class StringExtensions
    {

        public static bool IsDouble(this string st, string cost) =>
            !string.IsNullOrEmpty(st) && double.TryParse(cost, out _);

        public static bool IsValidDate(this string value, string[] dateFormats)
            => DateTime.TryParseExact(value, dateFormats, new CultureInfo("en-US"), DateTimeStyles.None, out _);
    }
}
