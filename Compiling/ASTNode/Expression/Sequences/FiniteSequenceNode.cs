using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class FiniteSequenceNode : Expression
    {
        List<Expression> seq;

        public FiniteSequenceNode(CodeLocation location, List<Expression> seq)
        {
            this.seq = seq;
            this.Location = location;
        }
        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {
            if (seq.Count == 0) return true;
            foreach (var item in seq)
            {
                if (!item.CheckSemantics(scope, errors))
                {
                    errors.Add(new CompilingError(item.Location, ErrorCode.Invalid, "Semantic"));
                    return false;
                }
            }
            return true;
        }

        public override IGeoEntities EvaluateOn(ScopeExecution scope)
        {
            if (seq.Count == 0)
            {
                return new Sequence();
            }

            List<IGeoEntities> toReturn = new List<IGeoEntities>();
            IGeoEntities first = seq[0].EvaluateOn(scope);

            if (first != null) toReturn.Add(first);
            else
            {
                scope.Errors.Add(new CompilingError(seq[0].Location, ErrorCode.Invalid, "Expression"));
                return null;
            }

            string type = first.Type();

            for (int i = 1; i < seq.Count; i++)
            {
                IGeoEntities aux = seq[i].EvaluateOn(scope);
                if (aux != null && aux.Type() == type)
                    toReturn.Add(aux);
                else
                {
                    scope.Errors.Add(new CompilingError(seq[i].Location, ErrorCode.Expected, "Expression type should be the same in the whole sequence"));
                    return null;
                }

            }

            return new Sequence(toReturn);
        }
    }
}
