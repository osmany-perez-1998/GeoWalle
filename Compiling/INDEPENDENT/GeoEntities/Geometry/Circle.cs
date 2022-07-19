using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class Circle:IGeometry,IPaintable,IIntersectable
    {
        public GeoPoint Center { get; private set; }
        public Measure Radius { get; private set; }
        public double D { get; private set; }
        public double E { get; private set; }
        public double F { get; private set; }
        public Circle(GeoPoint center,Measure radius)
        {
            Center = center;
            Radius = radius;
            D = -2 * center.X;
            E = -2 * center.Y;
            F = Math.Pow(center.X, 2) + Math.Pow(center.Y, 2) - Math.Pow(radius.Value, 2);
        }
        public string Type()
        {
            return "circle";
        }
    }
}
