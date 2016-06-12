using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests {
    [TestClass]
    public class LevelImageSolverTests : ImageSolverTests {
        [TestMethod]
        public void All_Levels() {
            _levels = LevelFactory.GetAll_Levels();
            Assert.AreEqual(480, _levels.Count);
            Run();
        }
        [TestMethod]
        public void Easy_Gallery_All() {
            _levels = LevelFactory.EasyGallery_All();
            Assert.AreEqual(80, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Easy_Gallery1() {
            _levels = LevelFactory.EasyGallery1();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Easy_Gallery2() {
            _levels = LevelFactory.EasyGallery2();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Easy_Gallery3() {
            _levels = LevelFactory.EasyGallery3();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Easy_Gallery4() {
            _levels = LevelFactory.EasyGallery4();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Medium_Gallery_All() {
            _levels = LevelFactory.MediumGallery_All();
            Assert.AreEqual(80, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Medium_Gallery1() {
            _levels = LevelFactory.MediumGallery1();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Medium_Gallery2() {
            _levels = LevelFactory.MediumGallery2();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Medium_Gallery3() {
            _levels = LevelFactory.MediumGallery3();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Medium_Gallery4() {
            _levels = LevelFactory.MediumGallery4();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Hard_Gallery_All() {
            _levels = LevelFactory.HardGallery_All();
            Assert.AreEqual(80, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Hard_Gallery1() {
            _levels = LevelFactory.HardGallery1();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Hard_Gallery2() {
            _levels = LevelFactory.HardGallery2();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Hard_Gallery3() {
            _levels = LevelFactory.HardGallery3();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Hard_Gallery4() {
            _levels = LevelFactory.HardGallery4();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Expert_Gallery_All() {
            _levels = LevelFactory.ExpertGallery_All();
            Assert.AreEqual(80, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Expert_Gallery1() {
            _levels = LevelFactory.ExpertGallery1();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Expert_Gallery2() {
            _levels = LevelFactory.ExpertGallery2();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Expert_Gallery3() {
            _levels = LevelFactory.ExpertGallery3();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Expert_Gallery4() {
            _levels = LevelFactory.ExpertGallery4();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Bonus1_Gallery_All() {
            _levels = LevelFactory.Bonus1Gallery_All();
            Assert.AreEqual(80, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Bonus1_Gallery1() {
            _levels = LevelFactory.Bonus1Gallery1();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Bonus1_Gallery2() {
            _levels = LevelFactory.Bonus1Gallery2();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Bonus1_Gallery3() {
            _levels = LevelFactory.Bonus1Gallery3();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Bonus1_Gallery4() {
            _levels = LevelFactory.Bonus1Gallery4();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Bonus2_Gallery_All() {
            _levels = LevelFactory.Bonus2Gallery_All();
            Assert.AreEqual(80, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Bonus2_Gallery1() {
            _levels = LevelFactory.Bonus2Gallery1();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Bonus2_Gallery2() {
            _levels = LevelFactory.Bonus2Gallery2();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Bonus2_Gallery3() {
            _levels = LevelFactory.Bonus2Gallery3();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }

        [TestMethod]
        public void Bonus2_Gallery4() {
            _levels = LevelFactory.Bonus2Gallery4();
            Assert.AreEqual(20, _levels.Count);
            Run();
        }
    }
}