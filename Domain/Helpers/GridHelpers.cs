using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Domain.Helpers {
    public static class GridHelpers {
        static GridHelpers() {
            ResetCache();
        }

        private static Dictionary<Color, string> _cache;

        public static void ResetCache() {
            _cache = new Dictionary<Color, string> { { Color.Empty, "_" }, { Color.FromArgb(0), "_" } };
            _cacheCounter = 0;
        }
        private static string validChars = "abcdefgh";

        private static int _cacheCounter;
        public static string ToReadableString(this Color[,] grid) {
            int rowCount = grid.GetLength(0);
            int colCount = grid.GetLength(1);
            var sb = new StringBuilder();
            for (int i = 0; i < rowCount; i++) {
                for (int j = 0; j < colCount; j++) {
                    if (!_cache.ContainsKey(grid[i, j])) {
                        var charToInsert = validChars[_cacheCounter++].ToString();
                        _cache[grid[i, j]] = charToInsert;
                    }
                    sb.Append(_cache[grid[i, j]]);
                    sb.Append(",");
                }
                sb.Length -= 1;
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public static List<Color> GetUsedColorsFromGrid(Color[,] grid) {
            var set = new List<Color>();
            foreach (var cell in grid) {
                set.Add(cell);
            }
            return set.Distinct().ToList();
        }

        public static Color[,] InitFromGridString(string gridString, out int rowCount, out int colCount) {
            var rows = gridString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            rowCount = rows.Length;
            colCount = rows[0].Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length;
            var grid = new Color[rowCount, colCount];
            int rowCounter = 0;
            foreach (var row in rows) {
                var columns = row.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                int columnCounter = 0;
                foreach (var column in columns) {
                    var colorInt = int.Parse(column);
                    Color color = Color.Empty;
                    if (colorInt != 0)
                        color = Color.FromArgb(colorInt);
                    grid[rowCounter, columnCounter] = color;
                    columnCounter++;
                }
                rowCounter++;
            }
            return grid;
        }

        public static Color[,] InitFromImg(string filePath, out int rowCount, out int colCount) {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);
            var bmp = new Bitmap(filePath);
            rowCount = bmp.Height;
            colCount = bmp.Width;
            var grid = new Color[bmp.Height, bmp.Width];
            for (int row = 0; row < bmp.Height; row++) {
                for (int col = 0; col < bmp.Width; col++) {
                    grid[row, col] = bmp.GetPixel(x: col, y: row);
                    if (grid[row, col].A == 0)
                        throw new Exception("Invalid color!");
                }
            }
            return grid;
        }

        public static Color[] GetRow(this Color[,] matrix, int n) {
            var colCount = matrix.GetLength(1);
            var res = new Color[colCount];
            for (int i = 0; i < colCount; i++) {
                res[i] = matrix[n, i];
            }
            return res;
        }

        public static Color[] GetColumn(this Color[,] matrix, int n) {
            var rowCount = matrix.GetLength(0);
            var res = new Color[rowCount];
            for (int i = 0; i < rowCount; i++) {
                res[i] = matrix[i, n];
            }
            return res;
        }
    }
}