using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Domain.Helpers;
using Domain.Picross;
using DColor = System.Drawing.Color;

namespace Domain.GuiWrappers {
    [ExcludeFromCodeCoverage]
    public class CellPresenter : Notifiable {
        public static bool ShowAllClassifiers = false;

        private Brush _myColor = Brushes.Transparent;
        public Brush MyColor {
            get { return _myColor; }
            set { SetNotify(() => MyColor, ref _myColor, value); }
        }

        private string _count = string.Empty;
        public string Count {
            get { return _count; }
            set { SetNotify(() => Count, ref _count, value); }
        }

        private bool _isConnected;
        public bool IsConnected {
            get { return _isConnected; }
            set { SetNotify(() => IsConnected, ref _isConnected, value); }
        }

        public Visibility IsConnectedVisibility {
            get { return IsConnected ? Visibility.Visible : Visibility.Collapsed; }
        }

        private bool _isDone;
        public bool IsDone {
            get { return _isDone; }
            set { SetNotify(() => IsDone, ref _isDone, value); }
        }

        public Visibility CellVisibility {
            get { return ShowAllClassifiers || !IsDone ? Visibility.Visible : Visibility.Hidden; }
        }

        public CellPresenter(DColor color) {
            if (color == DColor.Empty) {
                MyColor = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/img/checkers.png")));
            } else {
                MyColor = new SolidColorBrush(color.ToMediaColor());
            }
        }

        public CellPresenter(ColorClassifier value) {
            MyColor = new SolidColorBrush(value.MyColor.ToMediaColor());
            Count = value.Count.ToString();
            IsConnected = value.IsConnected;
            IsDone = value.IsDone;
        }

        public void NotifyVisibility() {
            Notify(() => CellVisibility);
        }
    }
}