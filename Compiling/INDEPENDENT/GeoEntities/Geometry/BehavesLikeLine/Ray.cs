using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class Ray:BehavesLikeLine
    {
        
        public GeoPoint StarsFrom { get; private set; }
        public GeoPoint GivenPoint { get; private set; }
        public Ray(GeoPoint p1, GeoPoint p2)
        {
            StarsFrom = p1;
            Pendiente = FindPendiente(p1, p2);
            GivenPoint = p2;
        }

        public override string Type()
        {
            return "ray";
        }
    }
}
