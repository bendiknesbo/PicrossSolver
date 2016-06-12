using System.Collections.Generic;
using Domain.Interfaces;
using Domain.Level;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.SolverTests {
    [TestClass]
    public class GridStringTests : SolverTestsBase {
        protected override void GridInit(string initializer) {
            Grid.InitFromGridString(initializer);
        }

        [TestMethod]
        public void GridString_1() {
            Levels = new List<ILevel>{
                new GridStringLevel("GridString: 1",@"
1,1,1,1,1
1,1,1,1,1
2,2,2,1,1
2,2,2,1,1
2,2,2,1,1
")
            };
            Run();
        }

        [TestMethod]
        public void GridString_2() {
            Levels = new List<ILevel>{
                new GridStringLevel("GridString: 2",@"
3,3,1,3,3
3,1,1,3,3
3,3,1,3,3
3,3,1,3,3
3,1,1,1,3
")
            };
            Run();
        }

        [TestMethod]
        public void GridString_3() {
            Levels = new List<ILevel>{
                new GridStringLevel("GridString: 3",@"
5,4,4,4,4
5,4,5,5,4
5,4,5,5,4
4,4,5,4,4
4,4,5,4,4
")
            };
            Run();
        }

        [TestMethod]
        public void GridString_4() {
            Levels = new List<ILevel>{
                new GridStringLevel("GridString: 4",@"
1,1,1,1,1
1,1,1,1,1
3,3,3,3,3
3,4,3,4,3
3,4,3,3,3
")
            };
            Run();
        }
    }
}