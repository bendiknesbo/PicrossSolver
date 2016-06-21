using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Domain.Helpers;

namespace Domain.Picross {
    public enum GridInitializerEnum {
        GridString,
        ImageFilePath
    }
    public class PicrossGrid {
        public int ColumnCount;
        public int RowCount;
        public List<Color> UsedColors;

        public Color[,] AnswerGrid;
        public List<Classifier> Rows = new List<Classifier>();
        public List<Classifier> Columns = new List<Classifier>();

        public PicrossGrid(string initString, GridInitializerEnum initializer) {
            switch (initializer) {
                case GridInitializerEnum.GridString:
                    AnswerGrid = GridHelpers.InitFromGridString(initString, rowCount: out RowCount, colCount: out ColumnCount);
                    break;
                case GridInitializerEnum.ImageFilePath:
                    AnswerGrid = GridHelpers.InitFromImg(initString, rowCount: out RowCount, colCount: out ColumnCount);
                    break;
                default: throw new Exception("Unknown initializer");
            }
            UsedColors = GridHelpers.GetUsedColorsFromGrid(AnswerGrid);
            if(UsedColors.Count > 4)
                throw new Exception(string.Format("To many colors detected! Detected a total of {0} colors", UsedColors.Count));
            GenerateRowClassifiers();
            GenerateColumnClassifiers();
        }


        private void GenerateRowClassifiers() {
            for (int i = 0; i < RowCount; i++) {
                var row = AnswerGrid.GetRow(i);
                Rows.Add(new Classifier(i, UsedColors, row));
            }
        }

        private void GenerateColumnClassifiers() {
            for (int i = 0; i < ColumnCount; i++) {
                var column = AnswerGrid.GetColumn(i);
                Columns.Add(new Classifier(i, UsedColors, column));
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