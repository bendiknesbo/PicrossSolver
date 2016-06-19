using System;
using Domain.Picross;

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

    }

}
