using System.Collections.Generic;
using Domain.Picross;

namespace Domain.Helpers {
    public class ColorClassifierIsSameColorComparer : IEqualityComparer<ColorClassifier> {
        public bool Equals(ColorClassifier x, ColorClassifier y) {
            return x.MyColor.Equals(y.MyColor);
        }

        public int GetHashCode(ColorClassifier cc) {
            return cc.MyColor.GetHashCode();
        }
    }
}