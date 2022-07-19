using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Compiling;

namespace GeoWallE
{
    public class RayDrawer:ISubDrawer
    {
        public RayDrawer(Drawer painter)
        {
            Register(typeof(Ray), painter);
        }
        public bool Paint(IGeoEntities entity, Canvas canvas, Color color, string label)
        {
            Ray aux = entity as Ray;
            canvas.SetColor(color);

            canvas.DrawRay(new Point((int)aux.StarsFrom.X, (int)aux.StarsFrom.Y), new Point((int)aux.GivenPoint.X, (int)aux.GivenPoint.Y), label);

            canvas.Refresh();
           

            return true;
        }

        public void Register(Type geoentity, Drawer painter)
        {
            painter.RegisterDrawer(geoentity, this);
        }
    }
}
