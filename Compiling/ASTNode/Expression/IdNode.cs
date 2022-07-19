using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class IdNode:Expression
    {
        public string Value { get; private set; }
        

        public IdNode(CodeLocation location, string value)
        {
            Value = value;
            this.Location = location;
        }

        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {
            bool use= scope.AlreadyAssignedVariable(Value);
            if (!use)
            {
                errors.Add(new CompilingError(Location, ErrorCode.Invalid, "Not assigned variable"));
                return false;
            }
            return true;
        }

        public override IGeoEntities EvaluateOn(ScopeExecution scope)
        {
            return scope.GetVariable(Value);
        }
    }
}
