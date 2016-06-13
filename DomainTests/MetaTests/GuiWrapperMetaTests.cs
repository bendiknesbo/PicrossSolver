using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Domain.GuiWrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.MetaTests {
    [TestClass]
    public class GuiWrapperMetaTests {
        [TestMethod]
        public void AllGuiWrappersAreExcludedFromCodeCoverage() {
            var assembly = Assembly.GetAssembly(typeof(GridPresenter));
            var presenterNamespace = typeof(GridPresenter).Namespace;
            Type[] guiWrappers = GetTypesInNamespace(assembly, presenterNamespace);
            var attribute = typeof(ExcludeFromCodeCoverageAttribute);
            foreach (Type type in guiWrappers) {
                Assert.IsNotNull(Attribute.GetCustomAttribute(type, attribute), string.Format("Type \"{0}\" did not have Attribute \"{1}\"", type.FullName, attribute.FullName));
            }
        }

        private Type[] GetTypesInNamespace(Assembly assembly, string nameSpace) {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }
    }
}