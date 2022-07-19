using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class FunctionCallNode : Expression
    {
        string identifier;
        List<Expression> args;
        
        public FunctionCallNode(CodeLocation location,string Identifier,List<Expression> Params)
        {
            identifier = Identifier;
            args = Params;
            this.Location = location;
        }
        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {
            bool use= scope.AlreadyAssignedFunction(identifier, new string[args.Count]);
            if (!use)
            {
                errors.Add(new CompilingError(Location, ErrorCode.Invalid, "Function was never declared"));
                return false;
            }
            return true;
        }

        public override IGeoEntities EvaluateOn(ScopeExecution scope)
        {
            ScopeExecution aux = new ScopeExecution(scope.Errors,scope.Drawer);
            aux.Colors = scope.Colors;
            foreach (var item in scope.Functions)
            {
                foreach (var element in item.Value)
                {
                    aux.AssignFunction(item.Key, element.Item1,element.Item2);
                }   
            }
            Tuple<string[], Expression> Function = scope.GetFunction(identifier, new string[args.Count]);

            for (int i = 0; i < args.Count; i++)
            {
                IGeoEntities exp = args[i].EvaluateOn(scope);
                if (exp == null)
                {
                    scope.Errors.Add(new CompilingError(Location, ErrorCode.Invalid, "Expression"));
                    return null;
                }
                aux.AssignVariable(Function.Item1[i], exp);
            }
            
            return Function.Item2.EvaluateOn(aux);

        }
    }
}
