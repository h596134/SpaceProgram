using System;
using System.Drawing;
namespace SpaceSim
{
    public class SpaceObject
    {
        public String Name { get; set; }
        public int OrbRadius { get; set; }
        public int OrbPeriod { get; set; }
        public int ObjRadius { get; set; }
        public int RotPeriod { get; set; }
        public Color Color { get; set; }

        public string GetName()
        {
            return Name;
        }
        public SpaceObject(String name, int orbRadius, int orbPeriod, int objRadius,
            int rotPeriod, Color color)
        {
            Name = name;
            this.OrbRadius = orbRadius;
            this.OrbPeriod = orbPeriod;
            this.ObjRadius = objRadius;
            this.RotPeriod = rotPeriod;
            this.Color = color;
        }
        public virtual void Draw()
        {
            Console.WriteLine(Name);
        }
    }
    public class Star : SpaceObject
    {
        public Star(String name, int orbRadius, int orbPeriod, int objRadius,
            int rotPeriod, Color color) : base(name, orbRadius, orbPeriod, objRadius
                , rotPeriod, color) { }
        public override void Draw()
        {
            Console.Write("Star : ");
            base.Draw();
        }
    }
    public class Planet : SpaceObject
    {
        public Planet(String name, int orbRadius, int orbPeriod, int objRadius,
            int rotPeriod, Color color) : base(name, orbRadius, orbPeriod, objRadius
                , rotPeriod, color) { }

        public int GetOrbRadius()
        {
            return OrbRadius;
        }

        public int GetOrbPeriod()
        {
            return OrbPeriod;
        }
        public override void Draw()
        {
            Console.Write("Planet: ");
            base.Draw();
        }

        // Beregne posisjonen til en planet ved en gitt tid 
        public void getPosition (double time) {
            double angle = 2 * Math.PI * time / OrbPeriod;
            double x = OrbRadius * Math.Cos(angle);
            double y = OrbRadius * Math.Sin(angle);

            Console.WriteLine($"X: {x}, Y: {y}");
        // Bruk orbital radius og orbital periode
        // X = radius * Cod(angle)
        // Y = radius * Sin(angle)
        }

    }
    public class Moon : Planet
    {
        public Planet OrbPlanet { get; private set; }
        public Moon(String name, Planet orbPlanet, int orbRadius, int orbPeriod, int objRadius,
            int rotPeriod, Color color) : 
            base(name, orbRadius, orbPeriod, objRadius, rotPeriod, color)
        {
            OrbPlanet = orbPlanet;
        }
        public override void Draw()
        {
            Console.Write("Moon : ");
            base.Draw();
        }
    }
    
}