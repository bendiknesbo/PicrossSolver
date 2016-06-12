using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Helpers;
using Domain.Interfaces;
using Domain.Picross;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.SolverTests {
    public abstract class SolverTestsBase {
        protected enum Step {
            Setup,
            Solve,
            Assert,
        }
        protected List<ILevel> Levels;

        protected PicrossGrid Grid;
        protected PicrossSolver Solver;

        protected abstract void GridInit(string initializer);

        protected void Setup(string initializer) {
            Grid = new PicrossGrid();
            GridInit(initializer);
            Solver = new PicrossSolver(Grid.RowCount, Grid.ColumnCount, Grid.Rows, Grid.Columns) {
                AnswerGrid = Grid.AnswerGrid
            };
        }

        protected void Run(bool doubleSolve = false) {
            var errorSb = new StringBuilder();
            var errorLevels = new List<string>();
            var inconclusiveLevels = new List<string>();
            var inconclusiveSb = new StringBuilder();
            foreach (var level in Levels) {
                GridHelpers.ResetCache();
                var levelName = level.Identifier;
                var initializer = level.Initializer;
                Step step = Step.Setup;
                try {
                    Setup(initializer);
                    step = Step.Solve;
                    Solver.Solve();
                    if (doubleSolve)
                        Solver.Solve();
                    step = Step.Assert;
                    AssertMatrix();
                } catch (Exception ex) {
                    if (ex.Message.Contains("Invalid color!")) {
                        inconclusiveLevels.Add(levelName);
                        AppendLog(inconclusiveSb, ex, step, level);
                    } else {
                        errorLevels.Add(levelName);
                        AppendLog(errorSb, ex, step, level);
                    }
                }
            }
            string totalOutput = Environment.NewLine;

            if (errorSb.Length != 0) {
                totalOutput += String.Format("Number of failing levels: {0}{1}", errorLevels.Count, Environment.NewLine);
                errorSb.AppendLine(HorizontalSplitter);
            }
            if (inconclusiveSb.Length != 0) {
                totalOutput += String.Format("Number of inconclusive levels: {0}{1}", inconclusiveLevels.Count, Environment.NewLine);
                inconclusiveSb.AppendLine(HorizontalSplitter);
            }
            totalOutput += HorizontalSplitter + errorSb + inconclusiveSb;
            if (errorLevels.Any()) {
                Assert.Fail(totalOutput);
            }
            if (inconclusiveLevels.Any()) {
                Assert.Inconclusive(totalOutput);
            }
        }

        protected const string HorizontalSplitter = "******************************************";


        private void AppendLog(StringBuilder sb, Exception ex, Step step, ILevel level) {
            sb.AppendLine();
            sb.AppendLine(String.Format("Failed during step: **{0}**", step));
            sb.AppendLine(String.Format("Failed during level: **{0}**", level.Identifier));
            var pathArray = level.Initializer.Split(new[] { @"\" }, StringSplitOptions.None);
            sb.AppendLine(String.Format(@"Specific level:      **LevelImages\{0}\{1}**", pathArray[pathArray.Length - 2], pathArray.Last()));
            if (step == Step.Assert) {
                sb.AppendLine(String.Format("Expected:{0}{1}", Environment.NewLine, Grid.AnswerGrid.ToReadableString()));
                sb.AppendLine(String.Format("Actual:   {0}{1}", Environment.NewLine, Solver.WorkingGrid.ToReadableString()));
            }
            sb.AppendLine(String.Format("Exception: {0}", ex));
            sb.AppendLine();
            sb.AppendLine(HorizontalSplitter);
        }

        public void AssertMatrix() {
            Assert.AreEqual(Grid.RowCount, Solver.WorkingGrid.GetLength(0));
            Assert.AreEqual(Grid.ColumnCount, Solver.WorkingGrid.GetLength(1));
            for (int i = 0; i < Grid.RowCount; i++) {
                for (int j = 0; j < Grid.ColumnCount; j++) {
                    Assert.AreEqual(Grid.AnswerGrid[i, j], Solver.WorkingGrid[i, j]);
                }
            }
        }
    }
}