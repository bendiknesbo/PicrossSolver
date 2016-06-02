using System;
using System.Drawing;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests {
    [TestClass]
    public class GridStringTests : SolverTestsBase {
        [TestMethod]
        public void Easy_Gallery1_0_0() {
            InitString = @"
1,1,1,1,1
1,1,1,1,1
2,2,2,1,1
2,2,2,1,1
2,2,2,1,1
";
            Setup();
            Assert.AreEqual(0, GetNumberOfElements(Solver.WorkingGrid, i => !i.Equals(Color.Empty)));
            Solver.Solve();
            Console.WriteLine(Solver.WorkingGrid.ToReadableString());
            Assert.AreNotEqual(0, GetNumberOfElements(Solver.WorkingGrid, i => !i.Equals(Color.Empty)));
            AssertMatrix();
        }

        [TestMethod]
        public void Easy_Gallery1_1_0() {
            InitString = @"
3,3,1,3,3
3,1,1,3,3
3,3,1,3,3
3,3,1,3,3
3,1,1,1,3
";
            Setup();
            Solver.Solve();
            Console.WriteLine(Solver.WorkingGrid.ToReadableString());
            AssertMatrix();
        }

        [TestMethod]
        public void Easy_Gallery1_2_0() {
            InitString = @"
5,4,4,4,4
5,4,5,5,4
5,4,5,5,4
4,4,5,4,4
4,4,5,4,4
";
            Setup();
            Solver.Solve();
            Console.WriteLine(Solver.WorkingGrid.ToReadableString());
            AssertMatrix();
        }

        [TestMethod]
        public void Easy_Gallery1_3_0() {
            InitString = @"
1,1,1,1,1
1,1,1,1,1
3,3,3,3,3
3,4,3,4,3
3,4,3,3,3
";
            Setup();
            Solver.Solve();
            Console.WriteLine(Solver.WorkingGrid.ToReadableString());
            AssertMatrix();
        }

        protected override void GridInit() {
            Grid.InitFromGridString(InitString);
        }
    }
}