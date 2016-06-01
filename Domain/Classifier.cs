using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Domain {
    public class Classifier {
        public Dictionary<Color, ColorClassifier> Colors = new Dictionary<Color, ColorClassifier>();

        public Classifier(List<Color> usedColors, Color[] row) {
            var colorRow = new Color[row.Length];
            for (int i = 0; i < row.Length; i++) {
                colorRow[i] = row[i];
            }
            foreach (Color color in usedColors) {
                var count = colorRow.Count(c => c == color);
                var firstIndex = Array.IndexOf(colorRow, color);
                var lastIndex = Array.LastIndexOf(colorRow, color);
                var isConnected = (lastIndex - firstIndex + 1) - count == 0;
                Colors.Add(color, new ColorClassifier { Count = count, MyColor = color, IsConnected = isConnected });
            }
        }
    }
}