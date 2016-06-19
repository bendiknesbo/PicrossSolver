using System;
using Domain.Helpers;
using Domain.Picross;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.TestBases {
    public abstract class BruteForcerTestsBase : SolverTestsBase {
        //protected PicrossSolver Solver; //todo: PicrossBruteForcer

        protected override void GridInit(string initializer) {
            Grid = new PicrossGrid();
            Grid.InitFromImg(initializer);
        }

        protected override void SolverInit() {
            Solver = new PicrossBruteForcer(Grid.RowCount, Grid.ColumnCount, Grid.Rows, Grid.Columns);
        }

        public override void AssertMatrix() {
            //BruteForcer is supposed to work on levels where multiple solutions exist, so we only care if there was a solution at all
            Assert.AreEqual(Grid.RowCount, Solver.WorkingGrid.GetLength(0));
            Assert.AreEqual(Grid.ColumnCount, Solver.WorkingGrid.GetLength(1));
            Assert.IsTrue(Solver.WorkingGrid.IsSolved(Solver.Rows, Solver.Columns));
        }
    }

}
