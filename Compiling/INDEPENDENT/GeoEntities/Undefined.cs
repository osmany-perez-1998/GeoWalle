using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class Undefined : IGeoEntities
    {
        public Undefined() { }
        public string Type()
        {
            return "undefined";
        }
    }
}
