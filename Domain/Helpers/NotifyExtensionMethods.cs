using System.ComponentModel;

namespace Domain.Helpers {
    public static class NotifyExtensionMethods {
        public static void Notify(this PropertyChangedEventHandler self, INotifyPropertyChanged sender, string propertyName) {
            if (self == null) return;
            self(sender, new PropertyChangedEventArgs(propertyName));
        }
    }
}