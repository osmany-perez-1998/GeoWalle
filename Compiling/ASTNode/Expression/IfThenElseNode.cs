using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class IfThenElseNode : Expression
    {
        Expression ifExp, thenExp, elseExp;
        

        public IfThenElseNode(CodeLocation location, Expression IfExp, Expression ThenExp, Expression ElseExp)
        {
            ifExp = IfExp;
            thenExp = ThenExp;
            elseExp = ElseExp;
            this.Location = location;

        }
        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {
            bool a1 = ifExp.CheckSemantics(scope, errors);
            bool a2 = thenExp.CheckSemantics(scope, errors);
            bool a3 = elseExp.CheckSemantics(scope, errors);
     

            if (!a1 || !a2 || !a3)
            {
                errors.Add(new CompilingError(ifExp.Location, ErrorCode.Invalid, "Semantic"));
                return false;
            }
            return true;
        }

        public override IGeoEntities EvaluateOn(ScopeExecution scope)

        {
            IGeoEntities a = ifExp.EvaluateOn(scope);

            if (a == null)
            {
                scope.Errors.Add(new CompilingError(ifExp.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }

            Number aux = a as Number;
            Undefined aux1 = a as Undefined;
            Sequence aux2 = a as Sequence;
            if ((aux!=null &&aux.Value==0) ||aux1 is Undefined || (aux2!=null && aux2.EmptySequence))
            {
                IGeoEntities else1= elseExp.EvaluateOn(scope);
                if (else1 == null)
                {
                    scope.Errors.Add(new CompilingError(elseExp.Location, ErrorCode.Invalid, "Expression"));
                    return null;
                }
                return else1;

            }
            IGeoEntities then= thenExp.EvaluateOn(scope);
            if (then == null)
            {
                scope.Errors.Add(new CompilingError(elseExp.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }
            return then;


        }
    }
}
