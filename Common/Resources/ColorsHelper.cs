using System.Windows.Media;

namespace ExifAnalyzer.Common
{
    public class ColorsHelper
    {
        public static SolidColorBrush MainGreen = new SolidColorBrush(Color.FromRgb(RedMainGreen, GreenMainGreen, BlueMainGreen));
        public static SolidColorBrush MouseOverGreen = new SolidColorBrush(Color.FromRgb(139, 216, 80));

        public static Color MainGreenColor = MainGreen.Color;
        public static Color MouseOverGreenColor = MouseOverGreen.Color;

        public const byte RedMainGreen = 76;
        public const byte GreenMainGreen = 127;
        public const byte BlueMainGreen = 38;
    }
}
