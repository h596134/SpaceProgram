using Microsoft.VisualBasic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Drawing;
using SpaceSim;
using System.Numerics;

namespace WPFApplication
{
    public partial class MainWindow : Window
    {
        // Define ellipses for planets
        private Star TheSun;
        private Planet Mercury;
        private Planet Venus;
        private Planet Earth;
        private Planet Mars;
        private Planet Jupiter;
        private Planet Saturn;
        private Planet Uranus;
        private Planet Neptune;
        private System.Windows.Point pos;
        private Ellipse starEllipse;
        private Dictionary<Ellipse, Planet> ellipseToPlanet = new Dictionary<Ellipse, Planet>();
        private Dictionary<Ellipse, Star> ellipseToStar = new Dictionary<Ellipse, Star>();
        private System.Windows.Point delta;
        public MainWindow()
        {
            InitializeComponent();
            InitializePlanets();
            InitializeComboBox();
            DispatcherTimer t = new()
            {
                Interval = TimeSpan.FromMilliseconds(100)
        };

            t.Tick += T_Tick;
            t.Start();
        }

        // Få bibben til å gå i sirkel
        private void T_Tick(object? sender, EventArgs e)
        {
           
            double time = DateTime.Now.TimeOfDay.TotalSeconds;

            foreach (var kvp in ellipseToPlanet)
            {
                Ellipse ellipse = kvp.Key;
                Planet planet = kvp.Value;
                
                double angle = 2 * Math.PI * time / (planet.OrbPeriod/10);

                // Calculate the new position based on the angle
                double x = GetOffset(planet) * Math.Cos(angle);
                double y = GetOffset(planet) * Math.Sin(angle);
                // pos.Y - planet.ObjRadius/1000
                double currentX = Canvas.GetLeft(ellipse);
                double currentY = Canvas.GetTop(ellipse);

                // Update the position of the ellipse relative to pos.X and pos.Y

                Canvas.SetLeft(ellipse, pos.X-ellipse.Width/2 + x);
                Canvas.SetTop(ellipse, pos.Y-ellipse.Width/2 + y);
             
            }
        }


        private double GetOffset(Planet planet)
        {
            switch (planet.Name)
            {
                case "Mercury":
                    return 110;
                case "Venus":
                    return 120;
                case "Earth":
                    return 140;
                case "Mars":
                    return 160;
                case "Jupiter":
                    return 250;
                case "Saturn":
                    return 390;
                case "Uranus":
                    return 480;
                case "Neptune":
                    return 540;
                default:
                    return 0;
            }
        }
        private void InitializePlanets()
        {
            draw.Width = draw.RenderSize.Width / 2;
            draw.Height = draw.RenderSize.Height / 2;
            pos.X = draw.Width;
            pos.Y = draw.Height;
            
            // Init Star
            TheSun = new Star("TheSun", 0, 0, 695700, 0, System.Drawing.Color.Yellow);
            // Initialize planets
            Planet Mercury = new Planet("Mercury", 28955, 88, 2440, 4224, System.Drawing.Color.Blue);
            Planet Venus = new Planet("Venus", 54100, 225, 6052, 5832, System.Drawing.Color.Orange);
            Planet Earth = new Planet("Earth", 149600, 365, 6371, 24, System.Drawing.Color.Green);
            Planet Mars = new Planet("Mars", 114000, 687, 3389, 24, System.Drawing.Color.Red);
            Planet Jupiter = new Planet("Jupiter", 389165, 4333, 69911, 9, System.Drawing.Color.Beige);
            Planet Saturn = new Planet("Saturn", 714700, 10759, 57232, 11, System.Drawing.Color.Honeydew);
            Planet Uranus = new Planet("Uranus", 1435495, 30685, 25362, 17, System.Drawing.Color.Aqua);
            Planet Neptune = new Planet("Neptune", 2257150, 60190, 24622, 16, System.Drawing.Color.Purple);


            // Add ellipses to the canvas
            AddStarToCanvas(TheSun);
            AddPlanetToCanvas(Mercury, pos.X + 110);
            AddPlanetToCanvas(Venus, pos.X + 120);
            AddPlanetToCanvas(Earth, pos.X + 140);
            AddPlanetToCanvas(Mars, pos.X + 160);
            AddPlanetToCanvas(Jupiter, pos.X+180);
            AddPlanetToCanvas(Saturn, pos.X + 330);
            AddPlanetToCanvas(Uranus, pos.X + 460);
            AddPlanetToCanvas(Neptune, pos.X + 520);
        }
        private void AddPlanetToCanvas(Planet planet, double left)
        {
            // Create a new Ellipse to represent the planet visually
            // double posX = pos.X + (planet.OrbRadius / 1000) + (planet.OrbRadius > 9999 ? 200 : 100);
            Ellipse planetEllipse = new Ellipse
            {
                Width = planet.ObjRadius/1000 * 2,
                Height = planet.ObjRadius/1000 * 2,
                Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(planet.Color.A, planet.Color.R, planet.Color.G, planet.Color.B))
            };

            // Add the ellipse to the canvas
            draw.Children.Add(planetEllipse);

            // Set the position of the ellipse using the provided Left and Top values
            Canvas.SetLeft(planetEllipse, left);
            Canvas.SetTop(planetEllipse, pos.Y - planet.ObjRadius/1000);

            ellipseToPlanet.Add(planetEllipse, planet);
        }

        private void AddStarToCanvas(Star star)
        {
            Ellipse starEllipse = new Ellipse
            {
                Width = 200,
                Height = 200,
                Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(star.Color.A, star.Color.R, star.Color.G, star.Color.B))
            };

            draw.Children.Add(starEllipse);
            Canvas.SetLeft(starEllipse, pos.X-100);
            Canvas.SetTop(starEllipse, pos.Y-100);

            // Add the star ellipse to the dictionary
            ellipseToStar.Add(starEllipse, star);
        }

        private void InitializeComboBox()
        {
            // Add ComboBox items for all planets
            foreach (var kvp in ellipseToPlanet)
            {
                Planet planet = kvp.Value;
                ComboBoxItem planetItem = new ComboBoxItem();
                planetItem.Content = planet.Name;
                PlanetComboBox.Items.Add(planetItem);
            }

            // Add the "All" item
            ComboBoxItem allItem = new ComboBoxItem();
            allItem.Content = "All";
            PlanetComboBox.Items.Add(allItem);
        }


        private void PlanetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            if (selectedItem != null)
            {
                string selectedPlanet = selectedItem.Content.ToString();
                //iterer gjennom planetene, 
                // Hide all planets
                foreach (var kvp in ellipseToPlanet)
                {
                    Ellipse ellipse = kvp.Key;
                    Planet planet = kvp.Value;

                    if (planet.Name == selectedPlanet)
                    {
                        ShowSelectedPlanet(selectedPlanet);
                        ellipse.Visibility = Visibility.Visible;
                    }
                    else if (selectedPlanet == "All")
                    {
                        ShowAllPlanets();
                    }
                    else
                    {
                        ellipse.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void ShowAllPlanets()
        {
            // Reset the size and visibility of all planets
            ResetAllPlanets();
            ResetSun(); // Reset the Sun's properties

            // Clear the canvas first
            draw.Children.Clear();

            // Define the offsets for each planet
            double[] offsets = { 110, 120, 140, 160, 180, 330, 460, 520 };

            // Add the Sun's ellipse
            Ellipse sunEllipse = new Ellipse
            {
                Width = 200,
                Height = 200,
                Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(TheSun.Color.A, TheSun.Color.R, TheSun.Color.G, TheSun.Color.B))
            };
            draw.Children.Add(sunEllipse);
            Canvas.SetLeft(sunEllipse, pos.X - 100);
            Canvas.SetTop(sunEllipse, pos.Y - 100);

            // Iterate through each entry in the dictionary
            int index = 0;
            foreach (var kvp in ellipseToPlanet)
            {
                Planet planet = kvp.Value;

                // Get the corresponding offset for the planet
                double offset = offsets[index];
                index++;

                // Calculate the left position using the offset
                double left = pos.X + offset;

                // Create a new ellipse for each planet and set its properties
                Ellipse planetEllipse = new Ellipse
                {
                    Width = planet.ObjRadius / 1000 * 2,
                    Height = planet.ObjRadius / 1000 * 2,
                    Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(planet.Color.A, planet.Color.R, planet.Color.G, planet.Color.B))
                };

                // Set the position of the ellipse
                Canvas.SetLeft(planetEllipse, left);
                Canvas.SetTop(planetEllipse, pos.Y - planet.ObjRadius / 1000);

                // Add the ellipse to the canvas
                draw.Children.Add(planetEllipse);
            }
        }

        private void ShowSelectedPlanet(string selectedPlanet)
        {
            // Hide all planets and labels except the selected one
            foreach (var kvp in ellipseToPlanet)
            {
                Ellipse ellipse = kvp.Key;
                Planet planet = kvp.Value;

                if (planet.Name == selectedPlanet)
                {
                    // Set width and height for the selected planet
                    ellipse.Width = 400;
                    ellipse.Height = 400;

                    // Set position
                    double left = pos.X - 200;
                    double top = pos.Y - 200;
                    Canvas.SetLeft(ellipse, left);
                    Canvas.SetTop(ellipse, top);

                    ellipse.Visibility = Visibility.Visible;
                }
                else
                {
                    // Hide other planets
                    ellipse.Visibility = Visibility.Collapsed;
                }
            }

            // Show the sun
            if (starEllipse != null)
            {
                starEllipse.Width = 400;
                starEllipse.Height = 400;
                Canvas.SetLeft(starEllipse, pos.X - 200);
                Canvas.SetTop(starEllipse, pos.Y - 200);
                starEllipse.Visibility = Visibility.Visible;
            }
        }




        private void ResetAllPlanets()
        {
            foreach (UIElement element in draw.Children)
            {
                if (element is Ellipse planet)
                {
                    planet.Width = GetDefaultWidth(planet.Name);
                    planet.Height = GetDefaultHeight(planet.Name);
                    planet.Visibility = Visibility.Visible;
                    // Reset the position if needed
                    double left = pos.X - (planet.Width / 2);
                    double top = pos.Y - (planet.Height / 2);
                    Canvas.SetLeft(planet, left);
                    Canvas.SetTop(planet, top);
                }
            }
        }
        private void ResetSun()
        {
            foreach (UIElement element in draw.Children)
            {
                if (element is Ellipse starEllipse)
                {
                    // Reset the properties of the star ellipse
                    starEllipse.Width = 200;
                    starEllipse.Height = 200;
                    Canvas.SetLeft(starEllipse, pos.X - 100);
                    Canvas.SetTop(starEllipse, pos.Y - 100);
                    starEllipse.Visibility = Visibility.Visible;
                }
            }
        }

        private void HideAllPlanetsExcept(Ellipse visiblePlanet)
        {
            foreach (UIElement element in draw.Children)
            {
                if (element is Ellipse && element != visiblePlanet)
                {
                    element.Visibility = Visibility.Collapsed;
                }
            }
        }
        private double GetDefaultWidth(string planetName)
        {
            switch (planetName)
            {
                case "TheSun":
                    return 200;
                case "Mercury":
                    return 4;
                case "Venus":
                    return 12;
                case "Earth":
                    return 12;
                case "Mars":
                    return 7;
                case "Jupiter":
                    return 140;
                case "Saturn":
                    return 114;
                case "Uranus":
                    return 50;
                case "Neptune":
                    return 51;
                default:
                    return 0;
            }
        }

        private double GetDefaultHeight(string planetName)
        {
            switch (planetName)
            {
                case "TheSun":
                    return 200;
                case "Mercury":
                    return 4;
                case "Venus":
                    return 12;
                case "Earth":
                    return 12;
                case "Mars":
                    return 7;
                case "Jupiter":
                    return 140;
                case "Saturn":
                    return 114;
                case "Uranus":
                    return 50;
                case "Neptune":
                    return 51;
                default:
                    return 0;
            }
        }

    }
        
    }
