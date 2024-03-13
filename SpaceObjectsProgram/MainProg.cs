using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using SpaceSim;


class Astronomy
{
    public static void Main()
    {
        // Creating spaceobjects
        // Stars
        Star sun = new Star("Sun", 0, 0, 695700, 0, Color.Yellow);

        // Planets
        Planet mercury = new Planet("Mercury", 28955, 88, 2440, 4224, Color.Blue);
        Planet venus = new Planet("Venus", 54100, 225, 6052, 5832, Color.Orange);
        Planet earth = new Planet("Earth", 149600, 365, 6371, 24, Color.Green);
        Planet mars = new Planet("Mars", 114000, 687, 3389, 24, Color.Red);
        Planet jupiter = new Planet("Jupiter", 389165, 4333, 69911, 9, Color.Beige);
        Planet saturn = new Planet("Saturn", 714700, 10759, 57232, 11, Color.Honeydew);
        Planet uranus = new Planet("Uranus", 1435495, 30685, 25362, 17, Color.Aqua);
        Planet neptune = new Planet("Neptune", 2257150, 60190, 24622, 16, Color.Purple);
        
        // Earth Moons
        Moon theMoon = new Moon("The Moon", earth, 15, 15, 15, 15, Color.Gray);

        // Mars Moons
        Moon phobos = new Moon("Phobos", mars, 9, 1, 11, 7, Color.Gray);
        Moon deimos = new Moon("Deimos", mars, 23, 2, 6, 12, Color.Gray);
        // name, planet, orbital radius, orbital period, objects radius, rotaitional period, color
        // Jupiter Moons
        Moon metis = new Moon("Metis", jupiter, 124, 1, 20, 1, Color.Gray);
        Moon adrastea = new Moon("Adrastea", jupiter, 124, 1, 10, 1, Color.Gray);
        Moon amalthea = new Moon("Amalthea", jupiter, 91,  1, 95, 1, Color.Gray);
        Moon thebe = new Moon("Thebe", jupiter, 111, 1, 50, 1, Color.Gray);
        Moon io = new Moon("Io", jupiter, 211, 2, 1815, 1, Color.Gray);
        Moon europa = new Moon("Europa", jupiter, 335, 4, 1560, 1, Color.Gray);
        Moon ganymede = new Moon("Ganymede", jupiter, 535, 7, 2631, 1, Color.Gray);
        Moon callisto = new Moon("Callisto", jupiter, 941, 17, 2400, 1, Color.Gray);
        Moon leda = new Moon("Leda", jupiter, 5500, 239, 8, 1, Color.Gray);
        Moon himalia = new Moon("Himalia", jupiter, 5740, 250, 93,1, Color.Gray);
        Moon lysithea = new Moon("Lysithea", jupiter, 5845, 259, 18, 1,Color.Gray);
        Moon elara = new Moon("Elara", jupiter, 5855, 259, 35,1,Color.Gray);
        Moon ananke = new Moon("Ananke", jupiter, 10600, -631, 15, 1, Color.Gray);
        Moon carme = new Moon("Carme", jupiter, 11800, -692, 20, 1, Color.Gray);
        Moon pasiphae = new Moon("Pasiphae", jupiter, 11750, -735, 25, 1, Color.Gray);
        Moon sinope = new Moon("Sinope", jupiter, 11850, -758, 18, 1, Color.Gray);



        // Adding Space Objects to a list
        List<SpaceObject> solarSystem = new List<SpaceObject>
        {
         // new XXX(name, orbRadius, orbPeriod, objRadius, rotPeriod, color)
            sun, mercury, venus,earth, theMoon,mars, jupiter, saturn,
            uranus, neptune, phobos, deimos, metis, adrastea, amalthea,
            thebe, io, europa, ganymede, callisto, leda, himalia,
            lysithea, elara, ananke, carme, pasiphae, sinope
        };

        // User input
        Console.Write("Enter the number of days since time 0: ");
        double time = double.Parse(Console.ReadLine());

        Console.Write("Enter the name of the planet (leave empty for Sun): ");
        string planetName = Console.ReadLine();

        // Find the selected planet or default to the sun
        SpaceObject selectedPlanet = string.IsNullOrWhiteSpace(planetName) ? sun : solarSystem.Find(obj => obj.Name == planetName);

        // Print details of the selected planet
        selectedPlanet.Draw();
        if (selectedPlanet is Planet)
        {
            Planet planet = (Planet)selectedPlanet;
            planet.getPosition(time);
        }
        // Print details of the moons belonging to the selected planet
        foreach (SpaceObject obj in solarSystem)
        {
            if (obj is Moon && ((Moon)obj).OrbPlanet == selectedPlanet)
            {
                obj.Draw();
                Moon moon = (Moon)obj;
                moon.getPosition(time);
            }
        }
       /* foreach (SpaceObject obj in solarSystem)
        {
            obj.Draw();

            // Check if the object is a Planet
            if (obj is Planet)
            {
                double time = 100; // 100 dager
                Planet planet = (Planet)obj; // Casting to Planet type
                planet.getPosition(time);
            }
        }
       */
        Console.ReadLine();
    }
}
