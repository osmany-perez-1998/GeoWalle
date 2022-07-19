using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE.IDrawers
{
    public class LineDrawer : ISubDrawer
    {
        public LineDrawer(Drawer painter )
        {
            Register(typeof(Line), painter);
        }
        public bool Paint(IGeoEntities entity, Canvas canvas, Color color, string label)
        {
            Line aux = entity as Line;
            canvas.SetColor(color);
            canvas.DrawLine(new Point((int)aux.p1.X, (int)aux.p1.Y), new Point((int)aux.p2.X, (int)aux.p2.Y), label);

            canvas.Refresh();

          

            return true;
        }

        public void Register(Type geoentity, Drawer painter)
        {
            painter.RegisterDrawer(geoentity, this);
        }
    }
}
