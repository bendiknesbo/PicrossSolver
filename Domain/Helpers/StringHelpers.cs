using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Domain.Helpers {
    public static class StringHelpers {
        public static string SplitCamelCase(this string s) {
            Regex upperCaseRegex = new Regex(@"[A-Z][a-z]*|\d+");
            MatchCollection matches = upperCaseRegex.Matches(s);
            List<string> words = new List<string>();
            foreach (Match match in matches) {
                words.Add(match.Value);
            }
            return String.Join(" ", words.ToArray());
        }
    }
}