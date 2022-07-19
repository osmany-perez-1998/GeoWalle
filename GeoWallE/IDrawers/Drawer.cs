using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;
using System.Reflection;
using System.Drawing;
using System.IO;

namespace GeoWallE
{
   public class Drawer:IDrawer
    {
        Canvas canvas;

        Dictionary<Type, ISubDrawer> toPaint;
        public Drawer(Canvas canvas)
        {
            this.canvas = canvas;
            toPaint = new Dictionary<Type, ISubDrawer>();

            string directory = Directory.GetCurrentDirectory();
            foreach (var file in Directory.GetFiles(directory))
            {
                if (Path.GetExtension(file) != ".exe" && Path.GetExtension(file) != ".dll")
                    continue;
                var library = Assembly.LoadFile(file);
                foreach (var type in library.GetTypes())
                {
                    if (type.IsClass && !type.IsAbstract && typeof(ISubDrawer).IsAssignableFrom(type))
                    Activator.CreateInstance(type, this);
                }
            }
        }

        public void Paint(IGeoEntities geo, string color, string label)
        {
            ISubDrawer drawer = toPaint[geo.GetType()];
            drawer.Paint(geo, canvas, Color.FromName(color), label);
        }

        public void RegisterDrawer(Type type, ISubDrawer painter)
        {
            toPaint[type] = painter;
        }
    }
}
