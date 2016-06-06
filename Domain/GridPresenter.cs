using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MColor = System.Windows.Media.Color;
using DColor = System.Drawing.Color;

namespace Domain {
    public class GridPresenter : Notifiable {
        private int _demoSelection;
        private PicrossGrid _grid;
        private PicrossSolver _solver;

        private List<RowPresenter> _rows;
        public List<RowPresenter> Rows {
            get { return _rows; }
            set { SetNotify(() => Rows, ref _rows, value); }
        }

        private List<ClassifierPresenter> _rowClassifiers;
        public List<ClassifierPresenter> RowClassifiers {
            get { return _rowClassifiers; }
            set { SetNotify(() => RowClassifiers, ref _rowClassifiers, value); }
        }

        private List<ClassifierPresenter> _columnClassifiers;
        public List<ClassifierPresenter> ColumnClassifiers {
            get { return _columnClassifiers; }
            set { SetNotify(() => ColumnClassifiers, ref _columnClassifiers, value); }
        }

        public void LoadDemoData() {
            var demoPath = string.Empty;
            switch (_demoSelection) {
                case 0:
                    demoPath = @"LevelImages\EasyGallery2\Small01.png";
                    break;
                case 1:
                    demoPath = @"LevelImages\EasyGallery2\Medium01.png";
                    break;
                case 2:
                    demoPath = @"LevelImages\EasyGallery2\Large01.png";
                    break;
                case 3:
                    demoPath = @"LevelImages\EasyGallery4\XLarge01.png";
                    break;
                default:
                    throw new Exception("Unknown _demoSelection");
            }
            _demoSelection++;
            if (_demoSelection == 4) _demoSelection = 0;
            LoadData(demoPath);
        }
        public void LoadSpecificData() {
            var specificPath = @"LevelImages\EasyGallery2\Large07.png";
            LoadData(specificPath);
        }

        private void LoadData(string path) {
            GridHelpers.ResetCache();
            _grid = new PicrossGrid();
            _grid.InitFromImg(path);
            _solver = new PicrossSolver(_grid.RowCount, _grid.ColumnCount, _grid.Rows, _grid.Columns);
            _solver.Solve();
            Rows = new List<RowPresenter>();
            for (int i = 0; i < _grid.RowCount; i++) {
                Rows.Add(new RowPresenter(_solver.WorkingGrid.GetRow(i)));
            }
            Notify(() => Rows);
            RowClassifiers = _solver.Rows.Select(r => new ClassifierPresenter(r)).ToList();
            ColumnClassifiers = _solver.Columns.Select(c => new ClassifierPresenter(c)).ToList();
        }

        public void NotifyClassifiers() {
            RowClassifiers.ForEach(c => c.NotifyVisibility());
            ColumnClassifiers.ForEach(c => c.NotifyVisibility());
        }
    }

    public abstract class RowPresenterBase : Notifiable {
        private List<CellPresenter> _cells;
        public List<CellPresenter> Cells {
            get { return _cells; }
            set { SetNotify(() => Cells, ref _cells, value); }
        }
    }

    public class RowPresenter : RowPresenterBase {
        public RowPresenter(DColor[] row) {
            Cells = row.Select(c => new CellPresenter(c)).ToList();
        }
    }

    public class ClassifierPresenter : RowPresenterBase {
        public ClassifierPresenter(Classifier classifier) {
            Cells = classifier.Colors.Select(c => new CellPresenter(c)).ToList();
        }

        public void NotifyVisibility() {
            Cells.ForEach(c => c.NotifyVisibility());
        }
    }

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

    public static class ColorExtensions {
        public static MColor ToMediaColor(this DColor color) {
            return MColor.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}