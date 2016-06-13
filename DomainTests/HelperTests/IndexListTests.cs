using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Domain.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.HelperTests {
    [TestClass]
    public class IndexListTests {
        [TestMethod]
        public void IndexListIsConnected() {
            var list = new IndexList { 1, 2, 3 };
            Assert.IsTrue(list.IsConnected);
            list = new IndexList { 3, 4, 5 };
            Assert.IsTrue(list.IsConnected);
            list = new IndexList { 1, 2 };
            Assert.IsTrue(list.IsConnected);
            list = new IndexList { 2 };
            Assert.IsTrue(list.IsConnected);
            list = new IndexList { 1, 2, 3, 4, 5, 6 };
            Assert.IsTrue(list.IsConnected);
            list = new IndexList { 6, 5, 4, 3, 2, 1 };
            Assert.IsTrue(list.IsConnected);
            list = new IndexList { 1, 6, 2, 5, 3, 4 };
            Assert.IsTrue(list.IsConnected);
        }

        [TestMethod]
        public void IndexListIsNotConnect() {
            var list = new IndexList { 1, 3 };
            Assert.IsFalse(list.IsConnected);
            list = new IndexList { 3, 5 };
            Assert.IsFalse(list.IsConnected);
            list = new IndexList { 1, 3, 4, 5 };
            Assert.IsFalse(list.IsConnected);
            list = new IndexList { 1, 3, 4, 5, 6 };
            Assert.IsFalse(list.IsConnected);
            list = new IndexList { 6, 4, 3, 2, 1 };
            Assert.IsFalse(list.IsConnected);
            list = new IndexList { 1, 5, 3, 4 };
            Assert.IsFalse(list.IsConnected);
        }
    }
}
