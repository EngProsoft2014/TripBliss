// Controls/FloatingActionButton.cs
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace TripBliss.Controls
{
    public class FloatingActionButton : ImageButton
    {
        public FloatingActionButton()
        {
            var fontImageSource = new FontImageSource
            {
                Glyph = "+", // Unicode for the plus icon in Material Icons
                FontFamily = "FontIconSolid",
                Color = Colors.White,
                Size = 25
            };

            Source = fontImageSource;
            BackgroundColor = Colors.Green;
            HeightRequest = 56;
            WidthRequest = 56;
            CornerRadius = 28;  // Assuming the FAB has a diameter of 56
            VerticalOptions = LayoutOptions.End;
            HorizontalOptions = LayoutOptions.End;
            Margin = new Thickness(16);
            Shadow = new Shadow
            {
                Brush = new SolidColorBrush(Colors.Black),
                Opacity = 0.5f,
                Offset = new Point(4, 4),
                Radius = 10
            };
        }
    }
}
