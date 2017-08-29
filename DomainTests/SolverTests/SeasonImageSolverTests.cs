using Domain.Picross;
using DomainTests.TestBases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.SolverTests {
    //[TestClass]
    public class SeasonImageSolverTests : ImageSolverTests {
        private const int ImagesPerYear = 52;
        private const int ImagesPerWeek = 9;
        private const int WholeYearsAdded = 2;
        private const int ExtraWeeksAdded = 0;
        [TestMethod]
        public void All_Seaons() {
            Levels = LevelFactory.GetAll_Seasons();
            Assert.AreEqual(ImagesPerYear * ImagesPerWeek * WholeYearsAdded + ExtraWeeksAdded * ImagesPerWeek, Levels.Count);
            Run();
        }
        [TestMethod]
        public void All_Seaons_Combined() {
            Levels = LevelFactory.GetAll_Seasons_Combined();
            Assert.AreEqual(ImagesPerYear * WholeYearsAdded, Levels.Count);
            Run();
        }
    }
}