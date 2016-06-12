using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Helpers;
using Domain.Interfaces;

namespace Domain.Picross {
    public static class LevelFactory {
        public static List<ILevel> GetAll_Levels() {
            var list = new List<ILevel>();
            FillListRecursive(Environment.CurrentDirectory + @"\LevelImages", list);
            return list;
        }
        public static void FillListRecursive(string dirPath, List<ILevel> list) {
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            LevelHelpers.FromFolder(dir.FullName, list);
            foreach (var childDir in dir.EnumerateDirectories()) {
                FillListRecursive(childDir.FullName, list);
            }
        }


        public static List<ILevel> GetAll_Test() {
            var list = new List<ILevel>();
            FillListRecursive(Environment.CurrentDirectory + @"\TestImages", list);
            return list;
        }

        public static List<ILevel> Get_Specific() {
            var list = new List<ILevel>();
            var fileInfo = new FileInfo(Properties.SpecificLevel);
            list.AddFileFromFolder(fileInfo);
            return list;
        }

        public static List<ILevel> EasyGallery_All() {
            return EasyGallery1().Union(EasyGallery2()).Union(EasyGallery3()).Union(EasyGallery4()).ToList();
        }
        public static List<ILevel> EasyGallery1() { return LevelHelpers.FromFolder(@"LevelImages\EasyGallery1\"); }
        public static List<ILevel> EasyGallery2() { return LevelHelpers.FromFolder(@"LevelImages\EasyGallery2\"); }
        public static List<ILevel> EasyGallery3() { return LevelHelpers.FromFolder(@"LevelImages\EasyGallery3\"); }
        public static List<ILevel> EasyGallery4() { return LevelHelpers.FromFolder(@"LevelImages\EasyGallery4\"); }

        public static List<ILevel> MediumGallery_All() {
            return MediumGallery1().Union(MediumGallery2()).Union(MediumGallery3()).Union(MediumGallery4()).ToList();
        }
        public static List<ILevel> MediumGallery1() { return LevelHelpers.FromFolder(@"LevelImages\MediumGallery1\"); }
        public static List<ILevel> MediumGallery2() { return LevelHelpers.FromFolder(@"LevelImages\MediumGallery2\"); }
        public static List<ILevel> MediumGallery3() { return LevelHelpers.FromFolder(@"LevelImages\MediumGallery3\"); }
        public static List<ILevel> MediumGallery4() { return LevelHelpers.FromFolder(@"LevelImages\MediumGallery4\"); }

        public static List<ILevel> HardGallery_All() {
            return HardGallery1().Union(HardGallery2()).Union(HardGallery3()).Union(HardGallery4()).ToList();
        }
        public static List<ILevel> HardGallery1() { return LevelHelpers.FromFolder(@"LevelImages\HardGallery1\"); }
        public static List<ILevel> HardGallery2() { return LevelHelpers.FromFolder(@"LevelImages\HardGallery2\"); }
        public static List<ILevel> HardGallery3() { return LevelHelpers.FromFolder(@"LevelImages\HardGallery3\"); }
        public static List<ILevel> HardGallery4() { return LevelHelpers.FromFolder(@"LevelImages\HardGallery4\"); }

        public static List<ILevel> ExpertGallery_All() {
            return ExpertGallery1().Union(ExpertGallery2()).Union(ExpertGallery3()).Union(ExpertGallery4()).ToList();
        }
        public static List<ILevel> ExpertGallery1() { return LevelHelpers.FromFolder(@"LevelImages\ExpertGallery1\"); }
        public static List<ILevel> ExpertGallery2() { return LevelHelpers.FromFolder(@"LevelImages\ExpertGallery2\"); }
        public static List<ILevel> ExpertGallery3() { return LevelHelpers.FromFolder(@"LevelImages\ExpertGallery3\"); }
        public static List<ILevel> ExpertGallery4() { return LevelHelpers.FromFolder(@"LevelImages\ExpertGallery4\"); }

        public static List<ILevel> Bonus1Gallery_All() {
            return Bonus1Gallery1().Union(Bonus1Gallery2()).Union(Bonus1Gallery3()).Union(Bonus1Gallery4()).ToList();
        }
        public static List<ILevel> Bonus1Gallery1() { return LevelHelpers.FromFolder(@"LevelImages\Bonus1Gallery1\"); }
        public static List<ILevel> Bonus1Gallery2() { return LevelHelpers.FromFolder(@"LevelImages\Bonus1Gallery2\"); }
        public static List<ILevel> Bonus1Gallery3() { return LevelHelpers.FromFolder(@"LevelImages\Bonus1Gallery3\"); }
        public static List<ILevel> Bonus1Gallery4() { return LevelHelpers.FromFolder(@"LevelImages\Bonus1Gallery4\"); }

        public static List<ILevel> Bonus2Gallery_All() {
            return Bonus2Gallery1().Union(Bonus2Gallery2()).Union(Bonus2Gallery3()).Union(Bonus2Gallery4()).ToList();
        }
        public static List<ILevel> Bonus2Gallery1() { return LevelHelpers.FromFolder(@"LevelImages\Bonus2Gallery1\"); }
        public static List<ILevel> Bonus2Gallery2() { return LevelHelpers.FromFolder(@"LevelImages\Bonus2Gallery2\"); }
        public static List<ILevel> Bonus2Gallery3() { return LevelHelpers.FromFolder(@"LevelImages\Bonus2Gallery3\"); }
        public static List<ILevel> Bonus2Gallery4() { return LevelHelpers.FromFolder(@"LevelImages\Bonus2Gallery4\"); }
    }
}