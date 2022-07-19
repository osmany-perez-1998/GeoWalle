using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class Segment:BehavesLikeLine
    {
        public GeoPoint Starts { get; private set; }
        public GeoPoint End { get; private set; }

     
        public Segment(GeoPoint p1,GeoPoint p2)
        {
            Starts = p1;
            End = p2;
            Pendiente = FindPendiente(p1, p2);
        }

        public override string Type()
        {
            return "segment";
        }
    }
}
