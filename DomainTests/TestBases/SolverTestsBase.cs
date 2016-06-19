using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Helpers;
using Domain.Interfaces;
using Domain.Level;
using Domain.Picross;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.TestBases {
    public abstract class SolverTestsBase {
        protected const string HorizontalSplitter = "******************************************";

        protected PicrossGrid Grid;
        protected List<ILevel> Levels;
        protected Action ManualSetup;
        protected SolverBase Solver;

        protected abstract void GridInit(string initializer);
        protected abstract void SolverInit();
        public abstract void AssertMatrix();

        protected void Setup(string initializer) {
            GridInit(initializer);
            SolverInit();
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
                var step = Step.Setup;
                try {
                    Setup(initializer);
                    step = Step.ManualSetup;
                    if (ManualSetup != null)
                        ManualSetup();
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
            var totalOutput = Environment.NewLine;

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


        protected void AppendLog(StringBuilder sb, Exception ex, Step step, ILevel level) {
            sb.AppendLine();
            sb.AppendLine(string.Format("Failed during step: **{0}**", step));
            sb.AppendLine(string.Format("Failed during level: **{0}**", level.Identifier));
            if (level is ImageLevel) {
                var pathArray = level.Initializer.Split(new[]{ @"\" }, StringSplitOptions.None);
                sb.AppendLine(string.Format(@"Specific level:      **LevelImages\{0}\{1}**", pathArray[pathArray.Length - 2], pathArray.Last()));
            }
            if (step == Step.Assert) {
                sb.AppendLine(string.Format("Expected:{0}{1}", Environment.NewLine, Grid.AnswerGrid.ToReadableString()));
                sb.AppendLine(string.Format("Actual:   {0}{1}", Environment.NewLine, Solver.WorkingGrid.ToReadableString()));
            }
            sb.AppendLine(string.Format("Exception: {0}", ex));
            sb.AppendLine();
            sb.AppendLine(HorizontalSplitter);
        }

        protected enum Step {
            Setup,
            ManualSetup,
            Solve,
            Assert
        }
    }
}