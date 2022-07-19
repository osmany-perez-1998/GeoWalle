using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class CountNode : Expression
    {
        Expression geo;
        
        public CountNode(CodeLocation location, Expression geo)
        {
            this.geo = geo;
            this.Location = location;
        }
        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {
            bool left = geo.CheckSemantics(scope, errors);        

            if (!left )
            {
                errors.Add(new CompilingError(geo.Location, ErrorCode.Invalid, "Semantic"));
                return false;
            }
            return true;
        }

        public override IGeoEntities EvaluateOn(ScopeExecution scope)
        {
            Sequence count = geo.EvaluateOn(scope) as Sequence;

            if (count == null)
            {
                scope.Errors.Add(new CompilingError(geo.Location, ErrorCode.Invalid, "Invalid parameter"));
                return null;
            }

            if (count.EmptySequence) return new Number();
            if (count.FiniteSequence || count.NumberIntervalSequence)
            {
                return new Number(count.FiniteStorage.Count);
            }

            else return new Undefined();
        }
    }
}
