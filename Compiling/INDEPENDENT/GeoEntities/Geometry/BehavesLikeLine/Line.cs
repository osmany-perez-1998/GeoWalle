using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class Line : BehavesLikeLine
    {
       public GeoPoint p1 { get; private set; }
        public GeoPoint p2 { get; private set; }
        public Line(GeoPoint p1,GeoPoint p2)
        {
            this.p1 = p1;
            this.p2 = p2;
            Pendiente = FindPendiente(p1, p2);
        }

        public override string Type()
        {
            return "line";
        }
    }
}
