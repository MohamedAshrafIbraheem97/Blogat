using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blogat
{
    public static class StringHandlers
    {
        public static string Limit(this string input, int max)
        {
            if (!String.IsNullOrEmpty(input) && input.Length > max)
            {
                return input.Substring(0, max);
            }
            return input;
        }

    }
}