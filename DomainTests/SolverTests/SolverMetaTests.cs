using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Domain.Picross;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.SolverTests {
    [TestClass]
    public class SolverMetaTests {
        /// <summary>
        /// This test verifies that all of the "Solve_Part_X" is added to the SolvePartActions-list.
        /// </summary>
        [TestMethod]
        public void AllSolvePartsAreCollectedInList() {
            var solver = new PicrossSolver(0, 0, new List<Classifier>(), new List<Classifier>());
            var solverType = typeof(PicrossSolver);
            var methods = solverType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(m => m.Name.StartsWith("Solve_Part_")).ToList();
            Assert.IsTrue(methods.Count > 0);
            Assert.AreEqual(methods.Count, solver.SolvePartActions.Count);
        }
    }
}
