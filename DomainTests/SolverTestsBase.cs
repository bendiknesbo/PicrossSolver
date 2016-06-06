using System;
using System.Drawing;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests {
    public abstract class SolverTestsBase {
        protected enum Step {
            Setup,
            Solve,
            Assert,
        }


        /// <summary>
        /// Can be a gridstring or a filepath
        /// </summary>
        protected string InitString { get; set; }

        protected PicrossGrid Grid;
        protected PicrossSolver Solver;

        protected abstract void GridInit();

        protected void Setup() {
            Grid = new PicrossGrid();
            GridInit();
            Solver = new PicrossSolver(Grid.RowCount, Grid.ColumnCount, Grid.Rows, Grid.Columns) {
                AnswerGrid = Grid.AnswerGrid
            };
        }

        public void AssertMatrix() {
            Assert.AreEqual(Grid.RowCount, Solver.WorkingGrid.GetLength(0));
            Assert.AreEqual(Grid.ColumnCount, Solver.WorkingGrid.GetLength(1));
            for (int i = 0; i < Grid.RowCount; i++) {
                for (int j = 0; j < Grid.ColumnCount; j++) {
                    Assert.AreEqual(Grid.AnswerGrid[i, j], Solver.WorkingGrid[i, j]);
                }
            }
        }

        public bool AreEqualMatrices(Color[,] expected, Color[,] actual) {
            if (expected.GetLength(0) != actual.GetLength(0)) return false;
            if (expected.GetLength(1) != actual.GetLength(1)) return false;
            for (int i = 0; i < Grid.RowCount; i++) {
                for (int j = 0; j < Grid.ColumnCount; j++) {
                    if (!(expected[i, j].Equals(actual[i, j])))
                        return false;
                }
            }
            return true;
        }

        public int GetNumberOfElements(Color[,] grid, Func<Color, bool> evaluator) {
            int count = 0;
            foreach (var cell in grid) {
                if (evaluator(cell))
                    count++;
            }
            return count;
        }
    }
}