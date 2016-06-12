using Domain.Interfaces;

namespace Domain.Level {
    public class GridStringLevel : ILevel {
        public GridStringLevel(string identifier, string gridString) {
            Identifier = identifier;
            Initializer = gridString;
        }
        public string Identifier { get; set; }
        public string Initializer { get; set; }
    }
}