namespace DomainTests.SolverTests {
    public class ImageSolverTests : SolverTestsBase {
        protected override void GridInit(string initializer) {
            Grid.InitFromImg(initializer);
        }
    }
}