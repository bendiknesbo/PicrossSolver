using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Domain.GuiWrappers;
using Domain.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.MetaTests {
    [TestClass]
    public class ExcludeFromCodeCoverageMetaTests {
        private readonly Type _attributeType = typeof(ExcludeFromCodeCoverageAttribute);

        [TestMethod]
        public void AllGuiWrappersAreExcludedFromCodeCoverage() {
            AllTypesInNamespaceAreExcludedFromCodeCoverage(typeof(GridPresenter));
        }

        [TestMethod]
        public void AllAnnotaionsAreExcludedFromCodeCoverage() {
            AllTypesInNamespaceAreExcludedFromCodeCoverage(typeof(CanBeNullAttribute));
        }

        private void AllTypesInNamespaceAreExcludedFromCodeCoverage(Type exampleType) {
            var assembly = Assembly.GetAssembly(exampleType);
            var presenterNamespace = exampleType.Namespace;
            Type[] typesInNamespace = GetTypesInNamespace(assembly, presenterNamespace);
            Assert.IsTrue(typesInNamespace.Length > 0); //to ensure the assembly was loaded correctly
            foreach (Type type in typesInNamespace) {
                Assert.IsNotNull(Attribute.GetCustomAttribute(type, _attributeType), string.Format("Type \"{0}\" did not have Attribute \"{1}\"", type.FullName, _attributeType.FullName));
            }
        }

        private Type[] GetTypesInNamespace(Assembly assembly, string nameSpace) {
            return assembly.GetTypes().Where(t => !t.IsEnum).Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }
    }
}