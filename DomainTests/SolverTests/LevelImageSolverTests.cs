using Domain.Picross;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.SolverTests {
    [TestClass]
    public class LevelImageSolverTests : ImageSolverTests {
        [TestMethod]
        public void SpecificLevelTest() {
            Levels = LevelFactory.Get_Specific();
            Assert.AreEqual(1, Levels.Count);
            Run(doubleSolve: true);
        }
        [TestMethod]
        public void All_Levels() {
            Levels = LevelFactory.GetAll_Levels();
            Assert.AreEqual(480, Levels.Count);
            Run();
        }
        [TestMethod]
        public void Easy_Gallery_All() {
            Levels = LevelFactory.EasyGallery_All();
            Assert.AreEqual(80, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Easy_Gallery1() {
            Levels = LevelFactory.EasyGallery1();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Easy_Gallery2() {
            Levels = LevelFactory.EasyGallery2();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Easy_Gallery3() {
            Levels = LevelFactory.EasyGallery3();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Easy_Gallery4() {
            Levels = LevelFactory.EasyGallery4();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Medium_Gallery_All() {
            Levels = LevelFactory.MediumGallery_All();
            Assert.AreEqual(80, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Medium_Gallery1() {
            Levels = LevelFactory.MediumGallery1();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Medium_Gallery2() {
            Levels = LevelFactory.MediumGallery2();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Medium_Gallery3() {
            Levels = LevelFactory.MediumGallery3();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Medium_Gallery4() {
            Levels = LevelFactory.MediumGallery4();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Hard_Gallery_All() {
            Levels = LevelFactory.HardGallery_All();
            Assert.AreEqual(80, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Hard_Gallery1() {
            Levels = LevelFactory.HardGallery1();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Hard_Gallery2() {
            Levels = LevelFactory.HardGallery2();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Hard_Gallery3() {
            Levels = LevelFactory.HardGallery3();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Hard_Gallery4() {
            Levels = LevelFactory.HardGallery4();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Expert_Gallery_All() {
            Levels = LevelFactory.ExpertGallery_All();
            Assert.AreEqual(80, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Expert_Gallery1() {
            Levels = LevelFactory.ExpertGallery1();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Expert_Gallery2() {
            Levels = LevelFactory.ExpertGallery2();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Expert_Gallery3() {
            Levels = LevelFactory.ExpertGallery3();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Expert_Gallery4() {
            Levels = LevelFactory.ExpertGallery4();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Bonus1_Gallery_All() {
            Levels = LevelFactory.Bonus1Gallery_All();
            Assert.AreEqual(80, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Bonus1_Gallery1() {
            Levels = LevelFactory.Bonus1Gallery1();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Bonus1_Gallery2() {
            Levels = LevelFactory.Bonus1Gallery2();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Bonus1_Gallery3() {
            Levels = LevelFactory.Bonus1Gallery3();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Bonus1_Gallery4() {
            Levels = LevelFactory.Bonus1Gallery4();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Bonus2_Gallery_All() {
            Levels = LevelFactory.Bonus2Gallery_All();
            Assert.AreEqual(80, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Bonus2_Gallery1() {
            Levels = LevelFactory.Bonus2Gallery1();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Bonus2_Gallery2() {
            Levels = LevelFactory.Bonus2Gallery2();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Bonus2_Gallery3() {
            Levels = LevelFactory.Bonus2Gallery3();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }

        [TestMethod]
        public void Bonus2_Gallery4() {
            Levels = LevelFactory.Bonus2Gallery4();
            Assert.AreEqual(20, Levels.Count);
            Run();
        }
    }
}