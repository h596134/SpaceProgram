using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFApp
{
    public partial class MainWindow : Window
    {
        private Ellipse TheSun;
        private Point pos;

        public MainWindow()
        {
            InitializeComponent();
            InitializePlanets();
        }

        private void InitializePlanets()
        {
            // Set canvas size to match window size
            draw.Width = draw.RenderSize.Width/2;
            draw.Height = draw.RenderSize.Height/2;
            pos.X = draw.Width;
            pos.Y = draw.Height;

            // Create ellipses
            TheSun = new Ellipse() { Name = "TheSun", Width = 200, Height = 200, Fill = Brushes.Yellow };

            // Add ellipse to the canvas
            draw.Children.Add(TheSun);

            Canvas.SetLeft(TheSun, pos.X - (TheSun.Width / 2));
            Canvas.SetTop(TheSun, pos.Y - (TheSun.Height / 2));
        }


    }
}
