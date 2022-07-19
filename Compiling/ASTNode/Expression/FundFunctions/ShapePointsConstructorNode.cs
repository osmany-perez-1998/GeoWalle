using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class ShapePointsConstructorNode:Expression
    {
        Expression body;
        
        public ShapePointsConstructorNode(CodeLocation location, Expression body)
        {
            this.body = body;
            this.Location = location;
        }

        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {
            return body.CheckSemantics(scope,errors);
        }

        public override IGeoEntities EvaluateOn(ScopeExecution scope)
        {
            throw new NotImplementedException();
        }
    }
}
