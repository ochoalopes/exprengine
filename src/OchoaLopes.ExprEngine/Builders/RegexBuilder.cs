﻿using System.Globalization;
using System.Text.RegularExpressions;

namespace OchoaLopes.ExprEngine.Builders
{
    public static class RegexBuilder
	{
        public static Regex BuildRegex(CultureInfo cultureInfo)
        {
            var decimalSeparator = Regex.Escape(cultureInfo.NumberFormat.NumberDecimalSeparator);
            var regex = new Regex(
                @"(:\w+|==|!=|>=|<=|<|>|&&|\|\||!|\+|-|\*|/|%|'[^']*'|true|false|\d+" + decimalSeparator + @"?\d*[idfD]?|\d*" + decimalSeparator + @"\d+[idfD]?|null|is\s+not\s+null|is\s+null|\(|\))",
            RegexOptions.IgnoreCase);

            return regex;
        }
    }
}