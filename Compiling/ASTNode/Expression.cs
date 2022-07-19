using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public abstract class Expression : ASTNode
    {
        public CodeLocation Location { get; set; }
        public abstract IGeoEntities EvaluateOn(ScopeExecution scope);

    }
}
