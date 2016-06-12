using System;
using System.Drawing;
using System.Linq;
using Domain.Picross;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.GridTests {
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
            Assert.AreEqual(2, grid.Rows[2].Colors.First(c => c.MyColor.Equals(Color.FromArgb(1))).Count);
            Assert.IsTrue(grid.Rows[2].Colors.First(c => c.MyColor.Equals(Color.FromArgb(1))).IsConnected);
            Assert.AreEqual(3, grid.Rows[2].Colors.First(c => c.MyColor.Equals(Color.FromArgb(2))).Count);
            Assert.IsTrue(grid.Rows[2].Colors.First(c => c.MyColor.Equals(Color.FromArgb(2))).IsConnected);
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
            Assert.AreEqual(4, grid.Rows[0].Colors.First(c => c.MyColor.Equals(Color.FromArgb(1))).Count);
            Assert.IsFalse(grid.Rows[0].Colors.First(c => c.MyColor.Equals(Color.FromArgb(1))).IsConnected);
            Assert.AreEqual(1, grid.Rows[0].Colors.First(c => c.MyColor.Equals(Color.FromArgb(2))).Count);
            Assert.IsTrue(grid.Rows[0].Colors.First(c => c.MyColor.Equals(Color.FromArgb(2))).IsConnected);
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

        [TestMethod]
        public void IsInitializable_Unsolvable1() {
            var grid = new PicrossGrid();
            var temp = @"
1,2,1,2
2,1,2,1
1,2,1,2 
2,1,2,1
";
            grid.InitFromGridString(temp);
            Assert.AreEqual(4, grid.Rows.Count);
            Assert.AreEqual(4, grid.Columns.Count);
        }
    }
}