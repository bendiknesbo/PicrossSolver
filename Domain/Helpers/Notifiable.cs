using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Domain.Properties;

namespace Domain.Helpers {
    [ExcludeFromCodeCoverage]
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
}