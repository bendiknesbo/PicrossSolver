using System;
using System.Drawing;
using System.IO;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Color = Domain.Color;

namespace DomainTests {
    [TestClass]
    public class GridTests {
        [TestMethod]
        public void Canary() {
            var grid = new PicrossGrid();
            var temp = @"
1,1,1,1,1
1,1,1,1,1
2,2,2,1,1
2,2,2,1,1
2,2,2,1,1
";
            grid.InitFromGridString(temp);
            Console.WriteLine(grid);
            Assert.AreEqual(5, grid.Rows.Count);
            Assert.AreEqual(5, grid.Columns.Count);
            Assert.AreEqual(2, grid.Rows[2].Colors[Color.Red].Count);
            Assert.IsTrue(grid.Rows[2].Colors[Color.Red].IsConnected);
            Assert.AreEqual(3, grid.Rows[2].Colors[Color.Blue].Count);
            Assert.IsTrue(grid.Rows[2].Colors[Color.Blue].IsConnected);
        }

        [TestMethod]
        public void Canary2() {
            var grid = new PicrossGrid();
            var temp = @"
1,2,1,1,1
";
            grid.InitFromGridString(temp);
            Console.WriteLine(grid);
            Assert.AreEqual(1, grid.Rows.Count);
            Assert.AreEqual(5, grid.Columns.Count);
            Assert.AreEqual(4, grid.Rows[0].Colors[Color.Red].Count);
            Assert.IsFalse(grid.Rows[0].Colors[Color.Red].IsConnected);
            Assert.AreEqual(1, grid.Rows[0].Colors[Color.Blue].Count);
            Assert.IsTrue(grid.Rows[0].Colors[Color.Blue].IsConnected);
        }

        [TestMethod]
        public void Test1() {
            var grid = new PicrossGrid();
            var temp = @"
1,1
";
            grid.InitFromGridString(temp);
            Assert.AreEqual(1, grid.Rows.Count);
            Assert.AreEqual(2, grid.Columns.Count);
        }

        [TestMethod]
        public void Test2() {
            var grid = new PicrossGrid();
            var temp = @"
1 
2
";
            grid.InitFromGridString(temp);
            Assert.AreEqual(2, grid.Rows.Count);
            Assert.AreEqual(1, grid.Columns.Count);
        }
    }

    [TestClass]
    public class HelperTests {
        [TestMethod]
        public void RowCountTest1() {
            var res = GridHelpers.GetRowCountFromGridString("1");
            Assert.AreEqual(1, res);
        }

        [TestMethod]
        public void RowCountTest2() {
            var res = GridHelpers.GetRowCountFromGridString("1,1");
            Assert.AreEqual(1, res);
        }

        [TestMethod]
        public void RowCountTest3() {
            var res = GridHelpers.GetRowCountFromGridString("1,2");
            Assert.AreEqual(1, res);
        }

        [TestMethod]
        public void RowCountTest4() {
            var res = GridHelpers.GetRowCountFromGridString(@"
1
");
            Assert.AreEqual(1, res);
        }

        [TestMethod]
        public void RowCountTest5() {
            var res = GridHelpers.GetRowCountFromGridString(@"
1,2
");
            Assert.AreEqual(1, res);
        }

        [TestMethod]
        public void RowCountTest6() {
            var res = GridHelpers.GetRowCountFromGridString(@"
1
2
");
            Assert.AreEqual(2, res);
        }

        [TestMethod]
        public void ColumnCountTest1() {
            var res = GridHelpers.GetColumnCountFromGridString("1");
            Assert.AreEqual(1, res);
        }

        [TestMethod]
        public void ColumnCountTest2() {
            var res = GridHelpers.GetColumnCountFromGridString("1,1");
            Assert.AreEqual(2, res);
        }

        [TestMethod]
        public void ColumnCountTest3() {
            var res = GridHelpers.GetColumnCountFromGridString("1,2");
            Assert.AreEqual(2, res);
        }

        [TestMethod]
        public void ColumnCountTest4() {
            var res = GridHelpers.GetColumnCountFromGridString(@"
1
");
            Assert.AreEqual(1, res);
        }

        [TestMethod]
        public void ColumnCountTest5() {
            var res = GridHelpers.GetColumnCountFromGridString(@"
1,2
");
            Assert.AreEqual(2, res);
        }

        [TestMethod]
        public void ColumnCountTest6() {
            var res = GridHelpers.GetColumnCountFromGridString(@"
1
2
");
            Assert.AreEqual(1, res);
        }
    }
}