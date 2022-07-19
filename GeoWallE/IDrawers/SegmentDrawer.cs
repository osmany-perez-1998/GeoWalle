using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE
{
    public class SegmentDrawer : ISubDrawer
    {
        public SegmentDrawer(Drawer painter)
        {
            Register(typeof(Segment),painter);
        }
        public bool Paint(IGeoEntities entity, Canvas canvas, System.Drawing.Color color, string label)
        {
            Segment toDraw = entity as Segment;

            canvas.SetColor(color);
            canvas.DrawSegment(new Point((int)toDraw.Starts.X, (int)toDraw.Starts.Y), new Point((int)toDraw.End.X, (int)toDraw.End.Y), label);

            canvas.Refresh();

          

            return true;
        }

        public void Register(Type geoentity, Drawer painter)
        {
            painter.RegisterDrawer(geoentity, this);
        }
    }
}
