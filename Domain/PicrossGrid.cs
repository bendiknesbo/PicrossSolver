using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Domain {
    public class PicrossGrid {
        public int ColumnCount { get; private set; }
        public int RowCount { get; private set; }

        public PicrossGrid() { }

        public int[,] AnswerGrid;
        public Dictionary<int, Classifier> Rows = new Dictionary<int, Classifier>();
        public Dictionary<int, Classifier> Columns = new Dictionary<int, Classifier>();


        public void InitFromGridString(string gridString) {
            RowCount = GridHelpers.GetRowCountFromGridString(gridString);
            ColumnCount = GridHelpers.GetColumnCountFromGridString(gridString);
            AnswerGrid = GridHelpers.InitFromGridString(gridString, rowCount: RowCount, colCount: ColumnCount);
            GenerateRowClassifiers();
            GenerateColumnClassifiers();
        }

        private void GenerateRowClassifiers() {
            for (int i = 0; i < RowCount; i++) {
                var row = AnswerGrid.GetRow(i);
                Rows.Add(i, new Classifier(row));
            }
        }

        private void GenerateColumnClassifiers() {
            for (int i = 0; i < ColumnCount; i++) {
                var column = AnswerGrid.GetColumn(i);
                Columns.Add(i, new Classifier(column));
            }
        }


        public override string ToString() {
            var res = new StringBuilder();
            res.AppendLine("WorkingGrid:" + Environment.NewLine + AnswerGrid.ToReadableString());
            res.AppendLine();
            res.AppendLine("RowClassifiers:" + Environment.NewLine + Rows.Count);
            res.AppendLine("ColumnClassifiers:" + Environment.NewLine + Columns.Count);
            return res.ToString();
        }
    }

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

    public class Classifier {
        public Dictionary<Color, ColorClassifier> Colors = new Dictionary<Color, ColorClassifier>();

        public Classifier(int[] row) {
            var colorRow = new Color[row.Length];
            for (int i = 0; i < row.Length; i++) {
                colorRow[i] = (Color) row[i];
            }
            foreach (Color color in Enum.GetValues(typeof(Color))) {
                var count = colorRow.Count(c => c == color);
                var firstIndex = Array.IndexOf(colorRow, color);
                var lastIndex = Array.LastIndexOf(colorRow, color);
                var isConnected = (lastIndex - firstIndex + 1) - count == 0;
                Colors.Add(color, new ColorClassifier { Count = count, MyColor = color, IsConnected = isConnected });
            }
        }
    }

    public class ColorClassifier {
        public Color MyColor { get; set; }
        public int Count { get; set; }
        public bool IsConnected { get; set; }
        public bool IsDone { get; set; }
    }
}