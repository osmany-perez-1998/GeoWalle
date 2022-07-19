using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiling;
namespace Compiling
{
    public interface IDrawer
    {
        void Paint(IGeoEntities topaint, string color, string label);
    }
}
