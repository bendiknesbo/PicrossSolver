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

    }
}
