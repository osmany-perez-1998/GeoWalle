using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class ColorNode: Statement
    {
        string color;
        CodeLocation location;
        public ColorNode(CodeLocation location, string color)
        {
            this.color = color;
            this.location = location;
        }
        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {
            return true;
        }

        public override void Execute(ScopeExecution scope, IDrawer drawer)
        {
            scope.Colors.Push(color);
        }
    }
}
