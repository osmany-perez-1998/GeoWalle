using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public abstract class BehavesLikeLine : IGeometry,IPaintable,IIntersectable
    {
        public double Ax { get; private set; }
        public double By { get; private set; }
        public double C { get; private set; }

        public double Pendiente { get;  set; }
        public double FindPendiente(GeoPoint a1, GeoPoint a2)
        {
            if (a1.X == a2.X)
            {
                Ax = 1;
                By = 0;               
                C = -a1.X;
                return long.MaxValue;
            }
            else
            {
                double m = (a1.Y - a2.Y) / (a1.X - a2.X);
                Ax = -(a1.Y-a2.Y);
                By = a1.X-a2.X;
                C = (a1.Y - a2.Y) * a1.X - (a1.X - a2.X) *(a1.Y);
                return m; 
            }
        }
        public abstract string Type();
    }
}
