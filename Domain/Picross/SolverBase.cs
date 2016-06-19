using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Domain.Picross {
    public abstract class SolverBase {

        public Color[,] WorkingGrid;
        public Color[,] AnswerGrid = null;
        protected readonly int RowCount;
        protected readonly int ColCount;
        protected readonly int CellCount;
        public List<Classifier> Rows { get; private set; }
        public List<Classifier> Columns { get; private set; }

        protected SolverBase(int rowCount, int colCount, List<Classifier> rows, List<Classifier> columns) {
            ColCount = colCount;
            RowCount = rowCount;
            CellCount = RowCount * ColCount;
            Columns = columns;
            Rows = rows;
            WorkingGrid = new Color[rowCount, colCount];
        }


        public abstract void Solve();
    }
}
