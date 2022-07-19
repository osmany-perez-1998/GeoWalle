using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class FunctionDeclaration : Statement
    {
        string identifier;
        string[] args;
        Expression body;
        CodeLocation location;

        public FunctionDeclaration(CodeLocation location, string identifier, string[] args, Expression body)
        {
            this.identifier = identifier;
            this.args = args;
            this.body = body;
            this.location = location;
        }
        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {

            bool use = scope.AlreadyAssignedFunction(identifier, args);
            if (use)
            {
                errors.Add(new CompilingError(location, ErrorCode.Invalid, "Function was never declared"));
                return false;
            }

            scope.AddFunctionDeclaration(identifier, args);

            ScopeCheckSemantic auxiliar = new ScopeCheckSemantic();

            foreach (var item in scope.FunctionsCheckSemantic)
            {
                auxiliar.FunctionsCheckSemantic.Add(item.Key, item.Value);
            }
            foreach (var item in args)
            {
                auxiliar.AddVariableDeclaration(item);
            }

            bool bod = body.CheckSemantics(auxiliar, errors);
            if (!bod)
            {
                errors.Add(new CompilingError(location, ErrorCode.Invalid, "Function Body"));
                return false;
            }
            return true;
        }

        public override void Execute(ScopeExecution scope, IDrawer drawer)
        {
            scope.AssignFunction(identifier, args, body);
        }
    }
}
