using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;
using System.Drawing;

namespace GeoWallE
{
    public interface ISubDrawer
    {
        bool Paint(IGeoEntities entity, Canvas canvas, System.Drawing.Color color, string label);
        void Register(Type geoentity, Drawer painter);
    }
}
