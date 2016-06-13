using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Domain.Helpers;

namespace Domain.GuiWrappers {
    [ExcludeFromCodeCoverage]
    public abstract class RowPresenterBase : Notifiable {
        private List<CellPresenter> _cells;
        public List<CellPresenter> Cells {
            get { return _cells; }
            set { SetNotify(() => Cells, ref _cells, value); }
        }
    }
}