using System.Diagnostics.CodeAnalysis;
using Domain.Picross;

namespace Domain.Helpers {
    [ExcludeFromCodeCoverage]
    public class IntRange {
        public int StartIndex = PicrossSolver.OutOfBoundsConst;
        public int EndIndex = PicrossSolver.OutOfBoundsConst;
    }
}