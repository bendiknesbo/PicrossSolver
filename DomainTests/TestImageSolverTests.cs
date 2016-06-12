using System.Linq;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests {
    [TestClass]
    public class TestImageSolverTests : ImageSolverTests {
        [TestMethod]
        public void All_Test() {
            _levels = LevelFactory.GetAll_Test();
            Run();
        }
    }
}