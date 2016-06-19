using Domain.Helpers;
using Domain.Picross;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.TestBases {
    public abstract class PicrossSolverTestsBase : SolverTestsBase {
        protected sealed override void SolverInit() {
            Solver = new PicrossSolver(Grid.RowCount, Grid.ColumnCount, Grid.Rows, Grid.Columns) {
                AnswerGrid = Grid.AnswerGrid
            };
        }
        public override void AssertMatrix() {
            //PicrossSolver is supposed to work on a single WorkingGrid, so we want to make sure that every color is correct.
            Assert.AreEqual(Grid.RowCount, Solver.WorkingGrid.GetLength(0));
            Assert.AreEqual(Grid.ColumnCount, Solver.WorkingGrid.GetLength(1));
            for (int i = 0; i < Grid.RowCount; i++) {
                for (int j = 0; j < Grid.ColumnCount; j++) {
                    Assert.AreEqual(Grid.AnswerGrid[i, j], Solver.WorkingGrid[i, j]);
                }
            }
        }
    }
}
