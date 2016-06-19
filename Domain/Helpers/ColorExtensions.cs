using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MColor = System.Windows.Media.Color;
using DColor = System.Drawing.Color;

namespace Domain.Helpers {
    [ExcludeFromCodeCoverage]
    public static class ColorExtensions {
        public static MColor ToMediaColor(this DColor color) {
            return MColor.FromArgb(color.A, color.R, color.G, color.B);
        }
        public static T DeepCopy<T>(this T obj) {
            BinaryFormatter s = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream()) {
                s.Serialize(ms, obj);
                ms.Position = 0;
                T t = (T) s.Deserialize(ms);

                return t;
            }
        }
    }
}