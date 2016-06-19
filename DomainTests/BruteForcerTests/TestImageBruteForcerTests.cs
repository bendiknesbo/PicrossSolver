using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Picross;
using DomainTests.TestBases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.BruteForcerTests {
    [TestClass]
    public class TestImageBruteForcerTests : BruteForcerTestsBase {
        [TestMethod]
        public void All_Test() {
            Levels = LevelFactory.GetAll_Unsolvable();
            Run();
        }

        [TestMethod]
        public void All_Levels() {
            Assert.Inconclusive("This test takes forever to bruteforce, some major optimizations are needed!");
            Levels = LevelFactory.GetAll_Levels();
            Run();
        }

    }
}
