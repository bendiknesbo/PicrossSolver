using Domain.Picross;

namespace DomainTests.TestBases {
    public class ImageSolverTests : PicrossSolverTestsBase {
        protected override void GridInit(string initializer) {
            Grid = new PicrossGrid();
            Grid.InitFromImg(initializer);
        }
    }
}