using Domain.Interfaces;

namespace Domain.Level {
    public class ImageLevel : ILevel {
        public ImageLevel(string identifier, string imagePath) {
            Identifier = identifier;
            Initializer = imagePath;
        }
        public string Identifier { get; set; }
        public string Initializer { get; set; }

    }
}