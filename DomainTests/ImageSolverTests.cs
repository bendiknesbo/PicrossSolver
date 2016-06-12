using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests {
    [TestClass]
    public class ImageSolverTests : SolverTestsBase {
        protected override void GridInit() {
            Grid.InitFromImg(InitString);
        }

        protected Dictionary<string, string> _levels;
        private const string HorizontalSplitter = "******************************************";

        protected void Run(bool doubleSolve = false) {
            var errorSb = new StringBuilder();
            var errorLevels = new List<string>();
            var inconclusiveLevels = new List<string>();
            var inconclusiveSb = new StringBuilder();
            foreach (var level in _levels) {
                GridHelpers.ResetCache();
                var levelName = level.Key;
                InitString = level.Value;
                Step step = Step.Setup;
                try {
                    Setup();
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
                totalOutput += string.Format("Number of failing levels: {0}{1}", errorLevels.Count, Environment.NewLine);
                errorSb.AppendLine(HorizontalSplitter);
            }
            if (inconclusiveSb.Length != 0) {
                totalOutput += string.Format("Number of inconclusive levels: {0}{1}", inconclusiveLevels.Count, Environment.NewLine);
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

        private void AppendLog(StringBuilder sb, Exception ex, Step step, KeyValuePair<string, string> level) {
            sb.AppendLine();
            sb.AppendLine(string.Format("Failed during step: **{0}**", step));
            sb.AppendLine(string.Format("Failed during level: **{0}**", level.Key));
            var pathArray = level.Value.Split(new[] { @"\" }, StringSplitOptions.None);
            sb.AppendLine(string.Format(@"Specific level:      **LevelImages\{0}\{1}**", pathArray[pathArray.Length - 2], pathArray.Last()));
            if (step == Step.Assert) {
                sb.AppendLine(string.Format("Expected:{0}{1}", Environment.NewLine, Grid.AnswerGrid.ToReadableString()));
                sb.AppendLine(string.Format("Actual:   {0}{1}", Environment.NewLine, Solver.WorkingGrid.ToReadableString()));
            }
            sb.AppendLine(string.Format("Exception: {0}", ex));
            sb.AppendLine();
            sb.AppendLine(HorizontalSplitter);
        }

        [TestMethod]
        public void All() {
            _levels = LevelFactory.GetAll();
            Run();
        }

        [TestMethod]
        public void SelectiveTest() {
            _levels = LevelFactory.GetAll_Levels().Where(kvp => kvp.Key.Contains("Easy Gallery 1: Medium 01")).ToDictionary();
            Assert.AreEqual(1, _levels.Count);
            Run(doubleSolve: true);
        }
    }
}