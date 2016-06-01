using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Domain {
    public static class GridHelpers {
        public static string ToReadableString(this Color[,] grid) {
            int rowCount = grid.GetLength(0);
            int colCount = grid.GetLength(1);
            var sb = new StringBuilder();
            for (int i = 0; i < rowCount; i++) {
                for (int j = 0; j < colCount; j++) {
                    sb.Append(grid[i, j].ToArgb() + ",");
                }
                sb.Length -= 1;
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public static int GetRowCountFromGridString(string gridString) {
            return gridString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static int GetColumnCountFromGridString(string gridString) {
            var rows = gridString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            return rows[0].Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static List<Color> GetUsedColorsFromGridString(string gridString) {
            var set = new List<Color>();
            var rows = gridString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var row in rows) {
                var cols = row.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(s => Color.FromArgb(int.Parse(s)));
                set.AddRange(cols);
            }
            return set.Distinct().ToList();
        }
        public static Color[,] InitFromGridString(string gridString, int rowCount, int colCount) {
            var rows = gridString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

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


        public static int GetRowCountFromImg(string filePath) {
            var bmp = new Bitmap(filePath);
            return bmp.Height;
        }

        public static int GetColumnCountFromImg(string filePath) {
            var bmp = new Bitmap(filePath);
            return bmp.Width;
        }

        public static List<Color> GetUsedColorsFromImg(string filePath) {
            var set = new List<Color>();
            var bmp = new Bitmap(filePath);
            for (int row = 0; row < bmp.Height; row++) {
                for (int col = 0; col < bmp.Width; col++) {
                    set.Add(bmp.GetPixel(row, col));
                }
            }
            return set.Distinct().ToList();
        }


        public static Color[,] InitFromImg(string filePath) {
            var bmp = new Bitmap(filePath);
            var grid = new Color[bmp.Height, bmp.Width];
            for (int row = 0; row < bmp.Height; row++) {
                for (int col = 0; col < bmp.Width; col++) {
                    grid[row, col] = bmp.GetPixel(row, col);
                }
            }
            return grid;
        }


        /*public static int[,] RotateMatrix(this int[,] matrix) {
            int newRowCount = matrix.GetLength(1);
            int newColCount = matrix.GetLength(0);
            int[,] res = new int[newRowCount, newColCount];
            for (int i = 0; i < newRowCount; i++) {
                for (int j = 0; j < newColCount; j++) {
                    var temp = matrix[j, i];
                    res[i, j] = temp;
                }
            }
            return res;
        }*/

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