using System;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests {
    [TestClass]
    public class SolverTests {
        private enum Step {
            Setup,
            Solve,
            Assert,
        }

        private PicrossSolver _solver;
        private string _gridString = @"
0,0,0,0,0
0,0,0,0,0
0,0,0,0,0
0,0,0,0,0
0,0,0,0,0
";
        private Dictionary<string, string> _levels;
        private PicrossGrid _grid;
        private const string HorizontalSplitter = "******************************************";

        [TestInitialize]
        public void TestInit() { }

        private void Setup(bool fromFile = false) {
            _grid = new PicrossGrid();
            if (fromFile)
                _grid.InitFromImg(_gridString);
            else
                _grid.InitFromGridString(_gridString);
            _solver = new PicrossSolver(_grid.RowCount, _grid.ColumnCount, _grid.Rows, _grid.Columns);
        }

        private void Run(bool fromFile = false) {
            var sb = new StringBuilder();
            var errorLevels = new List<string>();
            foreach (var level in _levels) {
                var levelName = level.Key;
                _gridString = level.Value;
                Step step = Step.Setup;
                try {
                    Setup(fromFile: fromFile);
                    step = Step.Solve;
                    _solver.Solve();
                    step = Step.Assert;
                    AssertMatrix();
                } catch (Exception ex) {
                    errorLevels.Add(levelName);
                    sb.AppendLine();
                    sb.AppendLine(string.Format("Failed during step: **{0}**", step));
                    sb.AppendLine(string.Format("Failed during level: **{0}**", levelName));
                    if (step == Step.Assert) {
                        var solvedOnce = (Color[,]) _solver.WorkingGrid.Clone();
                        _solver.Solve();
                        var solvedTwice = (Color[,]) _solver.WorkingGrid.Clone();
                        var expected = string.Format("Expected:{0}{1}", Environment.NewLine, _grid.AnswerGrid.ToReadableString());
                        var actual1 = string.Format("Actual:   {0}{1}", Environment.NewLine, solvedOnce.ToReadableString());
                        var actual2 = string.Format("Actual 2: {0}{1}", Environment.NewLine, solvedTwice.ToReadableString());
                        var solvedSecondTime = AreEqualMatrices(solvedTwice, _grid.AnswerGrid);
                        if (solvedSecondTime)
                            sb.AppendLine("WOW! Was correctly solved with second Solve()!");
                        sb.AppendLine(expected);
                        sb.AppendLine(actual1);
                        if (AreEqualMatrices(solvedOnce, solvedTwice))
                            sb.AppendLine("Nothing changed when attempting to solve a second time." + Environment.NewLine);
                        else
                            sb.AppendLine(actual2);
                    }
                    sb.AppendLine(string.Format("Exception: {0}", ex));
                    sb.AppendLine(HorizontalSplitter);
                }
            }
            if (sb.Length != 0) {
                sb.Insert(0, string.Format(@"
Number of failing levels: {0}
{1}
", errorLevels.Count, HorizontalSplitter));
                Assert.Fail(sb.ToString());
            }
        }

        [TestMethod]
        public void Easy_Gallery1() {
            _levels = LevelFactory.EasyGallery1();
            Run();
        }
        [TestMethod]
        public void Easy_Gallery1_FromImg() {
            _levels = LevelFactory.EasyGallery1_FromImg();
            Run(fromFile: true);
        }

        [TestMethod]
        public void GetRowCountFromImg() {
            var filePath = @"LevelImages\SizeTest.png";
            var rows = GridHelpers.GetRowCountFromImg(filePath);
            Assert.AreEqual(5, rows);
            var cols = GridHelpers.GetColumnCountFromImg(filePath);
            Assert.AreEqual(3, cols);
        }

        [TestMethod]
        public void Easy_Gallery2() {
            _levels = LevelFactory.EasyGallery2();
            Run();
        }
        [TestMethod]
        public void Medium_Gallery3() {
            _levels = LevelFactory.MediumGallery1();
            Run();
        }

        [TestMethod]
        public void SelectiveTest() {
            _levels = LevelFactory.EasyGallery1().Where(kvp => kvp.Key.Contains("Column 0, Row 2")).ToDictionary(k => k.Key, v => v.Value);
            Run();
        }


        [TestMethod]
        public void Easy_Gallery1_0_0() {
            _gridString = @"
1,1,1,1,1
1,1,1,1,1
2,2,2,1,1
2,2,2,1,1
2,2,2,1,1
";
            Setup();
            Assert.AreEqual(0, GetNumberOfElements(_solver.WorkingGrid, i => !i.Equals(Color.Empty)));
            _solver.Solve();
            Console.WriteLine(_solver.WorkingGrid.ToReadableString());
            Assert.AreNotEqual(0, GetNumberOfElements(_solver.WorkingGrid, i => !i.Equals(Color.Empty)));
            AssertMatrix();
        }

        [TestMethod]
        public void Easy_Gallery1_1_0() {
            _gridString = @"
3,3,1,3,3
3,1,1,3,3
3,3,1,3,3
3,3,1,3,3
3,1,1,1,3
";
            Setup();
            _solver.Solve();
            Console.WriteLine(_solver.WorkingGrid.ToReadableString());
            AssertMatrix();
        }

        [TestMethod]
        public void Easy_Gallery1_2_0() {
            _gridString = @"
5,4,4,4,4
5,4,5,5,4
5,4,5,5,4
4,4,5,4,4
4,4,5,4,4
";
            Setup();
            _solver.Solve();
            Console.WriteLine(_solver.WorkingGrid.ToReadableString());
            AssertMatrix();
        }

        [TestMethod]
        public void Easy_Gallery1_3_0() {
            _gridString = @"
1,1,1,1,1
1,1,1,1,1
3,3,3,3,3
3,4,3,4,3
3,4,3,3,3
";
            Setup();
            _solver.Solve();
            Console.WriteLine(_solver.WorkingGrid.ToReadableString());
            AssertMatrix();
        }

        public int GetNumberOfElements(Color[,] grid, Func<Color, bool> evaluator) {
            int count = 0;
            for (int i = 0; i < grid.GetLength(0); i++) {
                var row = grid.GetRow(i);
                for (int j = 0; j < row.Length; j++) {
                    if (evaluator(grid[i, j]))
                        count++;
                }
            }
            return count;
        }

        public void AssertMatrix() {
            Assert.AreEqual(_grid.RowCount, _solver.WorkingGrid.GetLength(0));
            Assert.AreEqual(_grid.ColumnCount, _solver.WorkingGrid.GetLength(1));
            for (int i = 0; i < _grid.RowCount; i++) {
                for (int j = 0; j < _grid.ColumnCount; j++) {
                    Assert.AreEqual(_grid.AnswerGrid[i, j], _solver.WorkingGrid[i, j]);
                }
            }
        }

        public bool AreEqualMatrices(Color[,] expected, Color[,] actual) {
            if (expected.GetLength(0) != actual.GetLength(0)) return false;
            if (expected.GetLength(1) != actual.GetLength(1)) return false;
            for (int i = 0; i < _grid.RowCount; i++) {
                for (int j = 0; j < _grid.ColumnCount; j++) {
                    if (!(expected[i, j].Equals(actual[i, j])))
                        return false;
                }
            }
            return true;
        }
    }
}