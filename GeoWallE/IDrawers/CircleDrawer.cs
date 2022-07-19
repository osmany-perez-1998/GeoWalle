using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;

namespace GeoWallE.IDrawers
{
    class CircleDrawer : ISubDrawer
    {
        public CircleDrawer(Drawer drawer)
        {
            Register(typeof(Circle), drawer);
        }
        public bool Paint(IGeoEntities entity, Canvas canvas, Color color, string label)
        {
            Circle toDraw = entity as Circle;
            canvas.SetColor(color);
            canvas.DrawCircle(new Point((int)toDraw.Center.X, (int)toDraw.Center.Y), (int)toDraw.Radius.Value, label);
            canvas.Refresh();
           
            return true;
        }

        public void Register(Type type, Drawer painter)
        {
            painter.RegisterDrawer(type, this);
        }

       
    }
}
