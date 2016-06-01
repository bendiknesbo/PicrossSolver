using System;
using System.Collections.Generic;
using Domain;

namespace DomainTests {

    public static class LevelFactory {
        //template:
        /*
          { "Column x, Row y", @"" },
          
          @"
0,0,0,0,0
0,0,0,0,0
0,0,0,0,0
0,0,0,0,0
0,0,0,0,0
"
          
         */
        public static Dictionary<string, string> EasyGallery1() {
            return new Dictionary<string, string>{
                { "Column 0, Row 0", "1,1,1,1,1\r\n1,1,1,1,1\r\n2,2,2,1,1\r\n2,2,2,1,1\r\n2,2,2,1,1" },
                { "Column 0, Row 1", "3,3,1,3,3\r\n3,1,1,3,3\r\n3,3,1,3,3\r\n3,3,1,3,3\r\n3,1,1,1,3" },
                { "Column 0, Row 2", "5,4,4,4,4\r\n5,4,5,5,4\r\n5,4,5,5,4\r\n4,4,5,4,4\r\n4,4,5,4,4" },
                { "Column 0, Row 3", "1,1,1,1,1\r\n1,1,1,1,1\r\n3,3,3,3,3\r\n3,4,3,4,3\r\n3,4,3,3,3" },
                { "Column 0, Row 4", "2,6,6,2,2\r\n2,6,6,6,2\r\n6,6,6,6,6\r\n5,5,5,5,5\r\n2,2,2,2,2" },
                { "Column 0, Row 5", "2,2,2,2,2\r\n7,2,7,2,7\r\n7,7,7,7,7\r\n7,7,8,7,7\r\n7,7,8,7,7" },
                { "Column 1, Row 0", "6,6,2,2,2\r\n4,4,6,2,2\r\n4,4,4,2,2\r\n4,4,4,4,4\r\n4,2,4,4,4" },
                { "Column 1, Row 1", "9,9,9,4,9\r\n9,9,9,4,9\r\n9,9,9,4,9\r\n9,9,9,4,9\r\n9,9,9,2,9\r\n9,9,9,2,9\r\n9,9,9,2,9\r\n9,9,9,2,9\r\n2,2,2,2,9\r\n2,2,2,9,9" },
                { "Column 1, Row 3", "8,8,8,8,8\r\n7,7,7,7,7\r\n6,8,7,8,6\r\n7,7,7,7,7\r\n7,7,8,7,7" },
                { "Column 1, Row 4", "3,3,1,1,1\r\n1,3,1,1,1\r\n1,3,1,1,1\r\n1,3,1,1,1\r\n1,3,1,1,1\r\n1,3,3,3,1\r\n1,3,3,3,3\r\n1,3,1,1,3\r\n1,3,1,1,3\r\n9,9,9,9,9" },
                { "Column 2, Row 0", "2,4,4,4,2\r\n2,4,4,4,2\r\n4,4,4,4,4\r\n2,3,3,3,2\r\n2,3,3,3,2" },
                { "Column 2, Row 1", "2,2,3,2,2\r\n2,3,9,3,2\r\n2,9,3,9,2\r\n2,9,9,9,2\r\n2,2,8,2,2" },
                { "Column 2, Row 2", "9,9,9,3,9\r\n9,9,9,3,9\r\n2,8,8,8,2\r\n2,9,9,9,2\r\n2,9,9,9,2" },
                { "Column 2, Row 3", "3,9,9,9,3\r\n3,9,3,9,3\r\n7,8,7,8,7\r\n7,8,8,8,7\r\n7,8,8,8,7" },
                { "Column 2, Row 4", "4,7,7,7,4\r\n4,2,1,2,4\r\n2,1,1,1,2\r\n2,1,1,1,2\r\n4,2,2,2,4" },
            }.WithPrefix("Easy Gallery 1:");
        }

        public static Dictionary<string, string> EasyGallery1_FromImg() {
            return new Dictionary<string, string>{
                { "Column 0, Row 0", @"LevelImages\EasyGallery1\Col0Row0.png" },
                }.WithPrefix("Easy Gallery 1:", fromFile: true);
        }

        public static Dictionary<string, string> EasyGallery2() {
            return new Dictionary<string, string>{
                { "Column 1, Row 2", "1,1,1,1,1,1,1,1,1,1\r\n2,1,1,1,2,1,1,1,1,1\r\n2,2,2,2,2,1,1,2,2,2\r\n2,2,2,2,2,1,1,2,1,2\r\n2,3,2,3,2,1,1,1,1,2\r\n2,2,2,2,2,1,1,1,1,2\r\n1,1,2,2,2,2,2,2,2,2\r\n1,1,2,2,2,2,2,2,2,2\r\n1,1,2,2,2,2,2,2,2,2\r\n1,1,2,2,1,1,1,1,1,2" },
            }.WithPrefix("Easy Gallery 2:");
        }

        public static Dictionary<string, string> MediumGallery1() {
            return new Dictionary<string, string>{
                { "Column 0, Row 3", "6,1,1,1,3,3,3,3,1,6\r\n6,1,1,3,1,3,3,1,1,6\r\n6,1,1,3,2,3,1,1,1,6\r\n1,6,1,1,2,1,1,1,6,1\r\n1,1,1,6,6,6,1,1,1,1\r\n1,1,1,6,6,6,1,1,1,1\r\n1,1,1,6,6,6,1,1,1,1\r\n1,1,6,6,6,6,1,2,2,2\r\n1,6,6,6,6,6,6,2,1,2\r\n9,9,9,9,9,6,9,9,9,9" },
            }.WithPrefix("Medium Gallery 2:");
        }
    }

    public static class DictionaryHelpers {
        public static Dictionary<string, string> WithPrefix(this Dictionary<string, string> dict, string prefix, bool fromFile = false) {
            var newDict = new Dictionary<string, string>();
            foreach (var row in dict) {
                if (string.IsNullOrWhiteSpace(row.Value))
                    continue;
                var newValue = row.Value;
                if (fromFile) {
                    //newValue = GridHelpers.InitFromImg(newValue);
                }
                if (newValue.Contains("0"))
                    throw new InvalidOperationException(string.Format("Picross solution can not contain zeroes! Level: {0}", prefix + " " + row.Key));
                newDict.Add(prefix + " " + row.Key, newValue);

            }
            return newDict;
        }

    }
}