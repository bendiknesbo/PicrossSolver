using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Domain {
    public static class GridHelpers {
        public static string ToReadableString(this int[,] grid) {
            int rowCount = grid.GetLength(0);
            int colCount = grid.GetLength(1);
            var sb = new StringBuilder();
            for (int i = 0; i < rowCount; i++) {
                for (int j = 0; j < colCount; j++) {
                    sb.Append(grid[i, j] + ",");
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
                var cols = row.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(s => (Color) int.Parse(s));
                set.AddRange(cols);
            }
            return set.Distinct().ToList();
        }
        public static int[,] InitFromGridString(string gridString, int rowCount, int colCount) {
            var rows = gridString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var grid = new int[rowCount, colCount];
            int rowCounter = 0;
            foreach (var row in rows) {
                var columns = row.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                int columnCounter = 0;
                foreach (var column in columns) {
                    grid[rowCounter, columnCounter] = int.Parse(column);
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

        public static System.Drawing.Color[,] InitFromImg(string filePath) {
            var bmp = new Bitmap(filePath);
            var grid = new System.Drawing.Color[bmp.Height, bmp.Width];
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

        public static int[] GetRow(this int[,] matrix, int n) {
            var colCount = matrix.GetLength(1);
            var res = new int[colCount];
            for (int i = 0; i < colCount; i++) {
                res[i] = matrix[n, i];
            }
            return res;
        }

        public static int[] GetColumn(this int[,] matrix, int n) {
            var rowCount = matrix.GetLength(0);
            var res = new int[rowCount];
            for (int i = 0; i < rowCount; i++) {
                res[i] = matrix[i, n];
            }
            return res;
        }
    }
}