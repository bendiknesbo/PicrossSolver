using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DColor = System.Drawing.Color;

namespace Domain.GuiWrappers {
    [ExcludeFromCodeCoverage]
    public class RowPresenter : RowPresenterBase {
        public RowPresenter(DColor[] row) {
            Cells = row.Select(c => new CellPresenter(c)).ToList();
        }
    }
}