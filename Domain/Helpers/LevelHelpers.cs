using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Interfaces;
using Domain.Level;

namespace Domain.Helpers {
    public static class LevelHelpers {
        public static List<ILevel> FromFolder(string folderPath, List<ILevel> existingList = null, string excludeFilename = null, string includeOnlyFilename = null) {
            var newList = existingList ?? new List<ILevel>();
            var dirInfo = new DirectoryInfo(folderPath);
            if (!dirInfo.Exists) throw new DirectoryNotFoundException();
            folderPath = folderPath.Split(new[] { @"\" }, StringSplitOptions.RemoveEmptyEntries).Last();
            var folderPrefix = folderPath.SplitCamelCase();
            foreach (var fileInfo in dirInfo.EnumerateFiles()) {
                if (excludeFilename != null && fileInfo.Name == excludeFilename)
                    continue;
                if (includeOnlyFilename != null && fileInfo.Name != includeOnlyFilename)
                    continue;
                newList.AddFileFromFolder(fileInfo, folderPrefix);
            }
            return newList;
        }

        public static void AddFileFromFolder(this List<ILevel> dict, FileInfo fileInfo, string folderPrefix = null) {
            if (!fileInfo.Extension.Contains(".png")) return;
            if (folderPrefix == null) {
                var folderPath = fileInfo.Directory.FullName.Split(new[] { @"\" }, StringSplitOptions.RemoveEmptyEntries).Last();
                folderPrefix = folderPath.SplitCamelCase();
            }
            var filePrefix = fileInfo.Name.SplitCamelCase();
            dict.Add(new ImageLevel(folderPrefix + ": " + filePrefix, fileInfo.FullName));
        }
    }
}