using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Domain.Helpers {
    [ExcludeFromCodeCoverage]
    public class IndexList : List<int> {
        public IndexList() : base() { }
        public IndexList(IEnumerable<int> collection) : base(collection) { }
        public bool IsConnected {
            get {
                Sort();
                return this.SequenceEqual(Enumerable.Range(this.First(), this.Last() - this.First() + 1));
            }
        }
    }
}