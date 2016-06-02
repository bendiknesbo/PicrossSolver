using System;
using System.Collections.Generic;
using System.IO;

namespace Domain {
    public static class LevelFactory {
        public static Dictionary<string, string> GetAll() {
            var dict = new Dictionary<string, string>();
            FillDictRecursive(Environment.CurrentDirectory, dict);
            return dict;
        }

        public static void FillDictRecursive(string dirPath, Dictionary<string, string> dict) {
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            DictionaryHelpers.FromFolder(dir.FullName, dict);
            foreach (var childDir in dir.EnumerateDirectories()) {
                FillDictRecursive(childDir.FullName, dict);
            }
        }

        public static Dictionary<string, string> EasyGallery1_Img() { return DictionaryHelpers.FromFolder(@"LevelImages\EasyGallery1\"); }
        public static Dictionary<string, string> EasyGallery2_Img() { return DictionaryHelpers.FromFolder(@"LevelImages\EasyGallery2\"); }
        public static Dictionary<string, string> EasyGallery3_Img() { return DictionaryHelpers.FromFolder(@"LevelImages\EasyGallery3\"); }
        public static Dictionary<string, string> EasyGallery4_Img() { return DictionaryHelpers.FromFolder(@"LevelImages\EasyGallery4\"); }

    }
}