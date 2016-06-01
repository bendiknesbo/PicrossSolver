using System;
using System.Collections.Generic;
using System.Text;

namespace Domain {
    public class PicrossGrid {
        public int ColumnCount { get; private set; }
        public int RowCount { get; private set; }
        public List<Color> UsedColors;

        public int[,] AnswerGrid;
        public Dictionary<int, Classifier> Rows = new Dictionary<int, Classifier>();
        public Dictionary<int, Classifier> Columns = new Dictionary<int, Classifier>();


        public void InitFromGridString(string gridString) {
            RowCount = GridHelpers.GetRowCountFromGridString(gridString);
            ColumnCount = GridHelpers.GetColumnCountFromGridString(gridString);
            UsedColors = GridHelpers.GetUsedColorsFromGridString(gridString);
            AnswerGrid = GridHelpers.InitFromGridString(gridString, rowCount: RowCount, colCount: ColumnCount);
            GenerateRowClassifiers();
            GenerateColumnClassifiers();
        }


        private void GenerateRowClassifiers() {
            for (int i = 0; i < RowCount; i++) {
                var row = AnswerGrid.GetRow(i);
                Rows.Add(i, new Classifier(UsedColors, row));
            }
        }

        private void GenerateColumnClassifiers() {
            for (int i = 0; i < ColumnCount; i++) {
                var column = AnswerGrid.GetColumn(i);
                Columns.Add(i, new Classifier(UsedColors, column));
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
}