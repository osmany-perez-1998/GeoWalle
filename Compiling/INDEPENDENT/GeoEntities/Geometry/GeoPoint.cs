using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class GeoPoint:IGeometry,IPaintable,IIntersectable
    {
        public double X { get; }
        public double Y { get; }
        static Random random = new Random();
        public GeoPoint()
        {
            X = random.Next(10, 650);
            Y = random.Next(10, 650);
        }
        public GeoPoint(double x, double y)
        {
                X = x;
                Y = y;
        }
        public string Type() { return "point"; }
    }
}
