using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE
{
    public class PointDrawer : ISubDrawer
    {
        public PointDrawer(Drawer drawer)
        {
            Register(typeof(GeoPoint), drawer);
        }
        public bool Paint(IGeoEntities entity, Canvas canvas, Color color, string label)
        {
            GeoPoint toDraw = entity as GeoPoint;

            canvas.SetColor(color);
            canvas.DrawPoint(new Point((int)toDraw.X, (int)toDraw.Y), label);
            canvas.Refresh();
            
            return true;
        }

        public void Register(Type geoentity, Drawer painter)
        {
            painter.RegisterDrawer(geoentity, this);
        }
    }
}
