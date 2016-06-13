using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Helpers {
    public static class NotifyExtensionMethods {
        [ExcludeFromCodeCoverage]
        public static void Notify(this PropertyChangedEventHandler self, INotifyPropertyChanged sender, string propertyName) {
            if (self == null) return;
            self(sender, new PropertyChangedEventArgs(propertyName));
        }
    }
}