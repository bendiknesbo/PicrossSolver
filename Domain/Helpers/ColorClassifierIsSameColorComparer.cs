using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Domain.Picross;

namespace Domain.Helpers {
    [ExcludeFromCodeCoverage]
    public class ColorClassifierIsSameColorComparer : IEqualityComparer<ColorClassifier> {
        public bool Equals(ColorClassifier x, ColorClassifier y) {
            return x.MyColor.Equals(y.MyColor);
        }

        public int GetHashCode(ColorClassifier cc) {
            return cc.MyColor.GetHashCode();
        }
    }
}