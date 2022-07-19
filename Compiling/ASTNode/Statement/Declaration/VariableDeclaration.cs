using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class VariableDeclaration : Statement
    {
        string identifier;
        Expression body;
        CodeLocation location;
        public VariableDeclaration(CodeLocation location,string identifier,Expression body)
        {
            this.identifier = identifier;
            this.body = body;
            this.location = location;
        }
        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {
            if (scope.AlreadyAssignedVariable(identifier))
            {
                errors.Add(new CompilingError(location, ErrorCode.Invalid, "Variable assigment"));
                return false;
            }
            if (!body.CheckSemantics(scope, errors))
            {
                errors.Add(new CompilingError(body.Location, ErrorCode.Invalid, "Expression"));
                return false;
            }
               scope.AddVariableDeclaration(identifier);

            return true;
        }

        public override void Execute(ScopeExecution scope, IDrawer drawer)
        {
            IGeoEntities aux = body.EvaluateOn(scope);
            if (aux == null)
            {
                scope.Errors.Add(new CompilingError(location, ErrorCode.Invalid, "Expression"));
                return;
            }
            scope.AssignVariable(identifier, aux);
        }
    }
}
