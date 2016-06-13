using System.Diagnostics.CodeAnalysis;
using MColor = System.Windows.Media.Color;
using DColor = System.Drawing.Color;

namespace Domain.Helpers {
    [ExcludeFromCodeCoverage]
    public static class ColorExtensions {
        public static MColor ToMediaColor(this DColor color) {
            return MColor.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}