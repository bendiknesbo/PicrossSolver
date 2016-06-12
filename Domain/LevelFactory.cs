using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Domain {
    public static class LevelFactory {
        public static Dictionary<string, string> GetAll_Levels() {
            var dict = new Dictionary<string, string>();
            FillDictRecursive(Environment.CurrentDirectory + @"\LevelImages", dict);
            return dict;
        }

        public static Dictionary<string, string> GetAll_Test() {
            var dict = new Dictionary<string, string>();
            FillDictRecursive(Environment.CurrentDirectory + @"\TestImages", dict);
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
            return EasyGallery1().Union(EasyGallery2()).Union(EasyGallery3()).Union(EasyGallery4()).ToDictionary();
        }
        public static Dictionary<string, string> EasyGallery1() { return DictionaryHelpers.FromFolder(@"LevelImages\EasyGallery1\"); }
        public static Dictionary<string, string> EasyGallery2() { return DictionaryHelpers.FromFolder(@"LevelImages\EasyGallery2\"); }
        public static Dictionary<string, string> EasyGallery3() { return DictionaryHelpers.FromFolder(@"LevelImages\EasyGallery3\"); }
        public static Dictionary<string, string> EasyGallery4() { return DictionaryHelpers.FromFolder(@"LevelImages\EasyGallery4\"); }

        public static Dictionary<string, string> MediumGallery_All() {
            return MediumGallery1().Union(MediumGallery2()).Union(MediumGallery3()).Union(MediumGallery4()).ToDictionary();
        }
        public static Dictionary<string, string> MediumGallery1() { return DictionaryHelpers.FromFolder(@"LevelImages\MediumGallery1\"); }
        public static Dictionary<string, string> MediumGallery2() { return DictionaryHelpers.FromFolder(@"LevelImages\MediumGallery2\"); }
        public static Dictionary<string, string> MediumGallery3() { return DictionaryHelpers.FromFolder(@"LevelImages\MediumGallery3\"); }
        public static Dictionary<string, string> MediumGallery4() { return DictionaryHelpers.FromFolder(@"LevelImages\MediumGallery4\"); }

        public static Dictionary<string, string> HardGallery_All() {
            return HardGallery1().Union(HardGallery2()).Union(HardGallery3()).Union(HardGallery4()).ToDictionary();
        }
        public static Dictionary<string, string> HardGallery1() { return DictionaryHelpers.FromFolder(@"LevelImages\HardGallery1\"); }
        public static Dictionary<string, string> HardGallery2() { return DictionaryHelpers.FromFolder(@"LevelImages\HardGallery2\"); }
        public static Dictionary<string, string> HardGallery3() { return DictionaryHelpers.FromFolder(@"LevelImages\HardGallery3\"); }
        public static Dictionary<string, string> HardGallery4() { return DictionaryHelpers.FromFolder(@"LevelImages\HardGallery4\"); }

        public static Dictionary<string, string> ExpertGallery_All() {
            return ExpertGallery1().Union(ExpertGallery2()).Union(ExpertGallery3()).Union(ExpertGallery4()).ToDictionary();
        }
        public static Dictionary<string, string> ExpertGallery1() { return DictionaryHelpers.FromFolder(@"LevelImages\ExpertGallery1\"); }
        public static Dictionary<string, string> ExpertGallery2() { return DictionaryHelpers.FromFolder(@"LevelImages\ExpertGallery2\"); }
        public static Dictionary<string, string> ExpertGallery3() { return DictionaryHelpers.FromFolder(@"LevelImages\ExpertGallery3\"); }
        public static Dictionary<string, string> ExpertGallery4() { return DictionaryHelpers.FromFolder(@"LevelImages\ExpertGallery4\"); }

        public static Dictionary<string, string> Bonus1Gallery_All() {
            return Bonus1Gallery1().Union(Bonus1Gallery2()).Union(Bonus1Gallery3()).Union(Bonus1Gallery4()).ToDictionary();
        }
        public static Dictionary<string, string> Bonus1Gallery1() { return DictionaryHelpers.FromFolder(@"LevelImages\Bonus1Gallery1\"); }
        public static Dictionary<string, string> Bonus1Gallery2() { return DictionaryHelpers.FromFolder(@"LevelImages\Bonus1Gallery2\"); }
        public static Dictionary<string, string> Bonus1Gallery3() { return DictionaryHelpers.FromFolder(@"LevelImages\Bonus1Gallery3\"); }
        public static Dictionary<string, string> Bonus1Gallery4() { return DictionaryHelpers.FromFolder(@"LevelImages\Bonus1Gallery4\"); }

        public static Dictionary<string, string> Bonus2Gallery_All() {
            return Bonus2Gallery1().Union(Bonus2Gallery2()).Union(Bonus2Gallery3()).Union(Bonus2Gallery4()).ToDictionary();
        }
        public static Dictionary<string, string> Bonus2Gallery1() { return DictionaryHelpers.FromFolder(@"LevelImages\Bonus2Gallery1\"); }
        public static Dictionary<string, string> Bonus2Gallery2() { return DictionaryHelpers.FromFolder(@"LevelImages\Bonus2Gallery2\"); }
        public static Dictionary<string, string> Bonus2Gallery3() { return DictionaryHelpers.FromFolder(@"LevelImages\Bonus2Gallery3\"); }
        public static Dictionary<string, string> Bonus2Gallery4() { return DictionaryHelpers.FromFolder(@"LevelImages\Bonus2Gallery4\"); }
    }
}