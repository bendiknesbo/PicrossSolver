using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Domain.Picross;

namespace Domain.GuiWrappers {
    [ExcludeFromCodeCoverage]
    public class ClassifierPresenter : RowPresenterBase {
        public ClassifierPresenter(Classifier classifier) {
            Cells = classifier.Colors.Select(c => new CellPresenter(c)).ToList();
        }

        public void NotifyVisibility() {
            Cells.ForEach(c => c.NotifyVisibility());
        }
    }
}