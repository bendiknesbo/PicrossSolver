using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Domain.Interfaces;
using Domain.Level;

namespace Domain.Helpers {
    [ExcludeFromCodeCoverage]
    public static class DictionaryHelpers {
        public static List<ILevel> WithPrefix(this List<ILevel> dict, string prefix) {
            var newList = new List<ILevel>();
            foreach (var row in dict) {
                if (string.IsNullOrWhiteSpace(row.Initializer))
                    continue;
                if (row.Initializer.Contains("0"))
                    throw new InvalidOperationException(string.Format("Picross solution can not contain zeroes! Level: {0}", prefix + " " + row.Identifier));
                newList.Add(new GridStringLevel(prefix + " " + row.Identifier, row.Initializer));
            }
            return newList;
        }

        public static Dictionary<T1, T2> ToDictionary<T1, T2>(this IEnumerable<KeyValuePair<T1, T2>> enumerableDict) {
            return enumerableDict.ToDictionary(k => k.Key, v => v.Value);
        }
    }
}