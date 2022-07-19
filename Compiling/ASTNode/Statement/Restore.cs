using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class Restore:Statement
    {
        public Restore()
        {

        }

        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {
            return true;
        }

        public override void Execute(ScopeExecution scope, IDrawer drawer)
        {
            if(scope.Colors.Count!=0)
            scope.Colors.Pop(); 
        }
    }
}
