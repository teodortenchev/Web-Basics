using System;

namespace SIS.HTTP.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this String str)
        {
            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }
    }
}
