using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Domain.Helpers;
using Domain.Picross;

namespace Domain.GuiWrappers {
    [ExcludeFromCodeCoverage]
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
            string demoPath;
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
            var specificPath = LevelFactory.Get_Specific().First().Initializer;
            LoadData(specificPath, doubleSolve: true);
        }

        private void LoadData(string path, bool doubleSolve = false) {
            GridHelpers.ResetCache();
            _grid = new PicrossGrid(path, GridInitializerEnum.ImageFilePath);
            _solver = new PicrossSolver(_grid.RowCount, _grid.ColumnCount, _grid.Rows, _grid.Columns);
            _solver.Solve();
            if (doubleSolve)
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
}