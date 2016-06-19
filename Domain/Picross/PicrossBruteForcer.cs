using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Picross {
    public class PicrossBruteForcer : SolverBase {
        public PicrossBruteForcer(int rowCount, int colCount, List<Classifier> rows, List<Classifier> columns) : base(rowCount, colCount, rows, columns) {}
        public override void Solve() {
            throw new NotImplementedException();
        }
    }
}
