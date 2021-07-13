using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SER.PayuSdk.Utils
{
    public static class StringExtension
    {
        // This converts to camel case
        // Location_ID => locationId, and testLEFTSide => testLeftSide

        public static string CamelCase(this string s)
        {
            var x = s.Replace("_", "");
            if (x.Length == 0) return "null";
            x = Regex.Replace(x, "([A-Z])([A-Z]+)($|[A-Z])",
                m => m.Groups[1].Value + m.Groups[2].Value.ToLower() + m.Groups[3].Value);
            return char.ToLower(x[0]) + x.Substring(1);
        }

        public static string PascalCase(this string s)
        {
            var x = CamelCase(s);
            return char.ToUpper(x[0]) + x.Substring(1);
        }
    }
}
