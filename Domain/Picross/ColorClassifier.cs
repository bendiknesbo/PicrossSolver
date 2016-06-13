using System.Drawing;

namespace Domain.Picross {
    public class ColorClassifier {
        public Color MyColor { get; set; }
        public int Count { get; set; }
        public bool IsConnected { get; set; }

        private bool _isDone;
        public bool IsDone {
            get { return _isDone || Count == 0; }
            set { _isDone = value; }
        }
    }
}