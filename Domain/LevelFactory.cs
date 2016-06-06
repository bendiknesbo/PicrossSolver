using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public static Dictionary<string, string> EasyGallery_All() {
            return EasyGallery1().Union(
                   EasyGallery2()).Union(
                   EasyGallery3()).Union(
                   EasyGallery4()).ToDictionary();
        }
        public static Dictionary<string, string> EasyGallery1() { return DictionaryHelpers.FromFolder(@"LevelImages\EasyGallery1\"); }
        public static Dictionary<string, string> EasyGallery2() { return DictionaryHelpers.FromFolder(@"LevelImages\EasyGallery2\"); }
        public static Dictionary<string, string> EasyGallery3() { return DictionaryHelpers.FromFolder(@"LevelImages\EasyGallery3\"); }
        public static Dictionary<string, string> EasyGallery4() { return DictionaryHelpers.FromFolder(@"LevelImages\EasyGallery4\"); }
        public static Dictionary<string, string> MediumGallery_All() {
            return MediumGallery1().Union(
                   MediumGallery2()).Union(
                   MediumGallery3()).Union(
                   MediumGallery4()).ToDictionary();
        }
        public static Dictionary<string, string> MediumGallery1() { return DictionaryHelpers.FromFolder(@"LevelImages\MediumGallery1\"); }
        public static Dictionary<string, string> MediumGallery2() { return DictionaryHelpers.FromFolder(@"LevelImages\MediumGallery2\"); }
        public static Dictionary<string, string> MediumGallery3() { return DictionaryHelpers.FromFolder(@"LevelImages\MediumGallery3\"); }
        public static Dictionary<string, string> MediumGallery4() { return DictionaryHelpers.FromFolder(@"LevelImages\MediumGallery4\"); }

    }
}