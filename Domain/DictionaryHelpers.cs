using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Domain {
    public static class DictionaryHelpers {
        public static Dictionary<string, string> FromFolder(string folderPath, Dictionary<string, string> existingDict = null) {
            var newDict = existingDict ?? new Dictionary<string, string>();
            var dirInfo = new DirectoryInfo(folderPath);
            if (!dirInfo.Exists) throw new DirectoryNotFoundException();
            folderPath = folderPath.Split(new[] { @"\" }, StringSplitOptions.RemoveEmptyEntries).Last();
            var folderPrefix = folderPath.SplitCamelCase();
            foreach (var fileInfo in dirInfo.EnumerateFiles()) {
                if (!fileInfo.Extension.Contains(".png")) continue;
                var filePrefix = fileInfo.Name.SplitCamelCase();
                newDict.Add(folderPrefix + ": " + filePrefix, fileInfo.FullName);

            }
            return newDict;
        }
        private static string SplitCamelCase(this string s) {
            Regex upperCaseRegex = new Regex(@"[A-Z][a-z]*|\d+");
            MatchCollection matches = upperCaseRegex.Matches(s);
            List<string> words = new List<string>();
            foreach (Match match in matches) {
                words.Add(match.Value);
            }
            return String.Join(" ", words.ToArray());
        }

        public static Dictionary<string, string> WithPrefix(this Dictionary<string, string> dict, string prefix) {
            var newDict = new Dictionary<string, string>();
            foreach (var row in dict) {
                if (string.IsNullOrWhiteSpace(row.Value))
                    continue;
                if (row.Value.Contains("0"))
                    throw new InvalidOperationException(string.Format("Picross solution can not contain zeroes! Level: {0}", prefix + " " + row.Key));
                newDict.Add(prefix + " " + row.Key, row.Value);
            }
            return newDict;
        }

    }
}