using Domain.Picross;
using DomainTests.TestBases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.SolverTests {
    [TestClass]
    public class TestImageSolverTests : ImageSolverTests {
        [TestMethod]
        public void All_Test() {
            Levels = LevelFactory.GetAll_Test();
            Run();
        }
    }
}