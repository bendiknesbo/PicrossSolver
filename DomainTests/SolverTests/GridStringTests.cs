using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Domain.Helpers;
using Domain.Interfaces;
using Domain.Level;
using Domain.Picross;
using DomainTests.TestBases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.SolverTests {
    [TestClass]
    public class GridStringSolverTests : PicrossSolverTestsBase {
        protected override void GridInit(string initializer) {
            Grid = new PicrossGrid();
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
        [TestMethod]
        public void GridString_Unsolvable_1() {
            string levelString = @"
1,2,1,2
2,1,2,1
1,2,1,2 
2,1,2,1
";
            Levels = new List<ILevel>{
                new GridStringLevel("Unsolvable: 1",levelString)
            };
            try {
                Run();
            } catch (AssertFailedException) {
                for (int i = 0; i < Grid.RowCount; i++) {
                    for (int j = 0; j < Grid.ColumnCount; j++) {
                        Grid.AnswerGrid[i, j] = Color.Empty;
                    }
                }
                AssertMatrix();
            }
        }


        [TestMethod]
        public void FillFromLastIndex() {
            Levels = new List<ILevel>{
                new GridStringLevel("ManualTest1: 1", "2,2,2,1,1")
            };
            ManualSetup = () => {
                Solver.WorkingGrid[0, 4] = Color.FromArgb(1);
                Solver.Rows.Single().Colors.Reverse(); //reverse order so solver starts with color "1" instead of first encountered: "2"
            };
            Run();
        }
    }
}