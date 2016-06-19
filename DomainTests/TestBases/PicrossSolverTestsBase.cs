using Domain.Picross;

namespace DomainTests.TestBases {
    public abstract class PicrossSolverTestsBase : SolverTestsBase {
        protected sealed override void SolverInit() {
            Solver = new PicrossSolver(Grid.RowCount, Grid.ColumnCount, Grid.Rows, Grid.Columns) {
                AnswerGrid = Grid.AnswerGrid
            };
        }
    }
}
