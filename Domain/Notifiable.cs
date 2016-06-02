using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Domain.Annotations;

namespace Domain {
    public abstract class Notifiable : INotifyPropertyChanged {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool SetNotify<T>(Expression<Func<T>> property, ref T field, T value) {
            if (IsUnchanged(field, value)) return false;
            field = value;
            Notify(property);
            return true;
        }
        protected void Notify<T>(Expression<Func<T>> property) {
            var memberExpr = property.Body as MemberExpression;
            PropertyChanged.Notify(this, memberExpr.Member.Name);
        }
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName) {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        private static bool IsUnchanged<T>(T field, T value) {
            if (field == null && value == null) return true;
            return ((value is ValueType || value != null) && value.Equals(field));
        }
    }

    public static class NotifyExtensionMethods {
        public static void Notify(this PropertyChangedEventHandler self, INotifyPropertyChanged sender, string propertyName) {
            if (self == null) return;
            self(sender, new PropertyChangedEventArgs(propertyName));
        }
    }
}
