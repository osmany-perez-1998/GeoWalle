using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class LetInNode:Expression
    {
        List<Statement> let;
        Expression inExp;
        
        public LetInNode(CodeLocation location, List<Statement> let, Expression inExp)
        {
            this.let = let;
            this.inExp = inExp;
            this.Location = location;
        }

        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {
            foreach (var item in let)
            {
                if (!item.CheckSemantics(scope, errors))
                {
                    errors.Add(new CompilingError(Location, ErrorCode.Invalid, "Statement"));
                    return false;
                }
            }
        
            bool inexp= inExp.CheckSemantics(scope,errors);
            if (!inexp)
            {
                errors.Add(new CompilingError(Location, ErrorCode.Invalid, "Expression"));
            }
            return true;
        }

        public override IGeoEntities EvaluateOn(ScopeExecution scope)
        {
            ScopeExecution aux = new ScopeExecution(scope.Errors,scope.Drawer,scope);
            aux.Colors = scope.Colors;
            foreach (var item in scope.Functions)
            {
                foreach (var element in item.Value)
                {
                    aux.AssignFunction(item.Key, element.Item1, element.Item2);
                }

            }

            int initial = scope.Errors.Count;
            foreach (var item in let)
            {
                item.Execute(aux, aux.Drawer);
                if (scope.Errors.Count != initial)
                {
                    return null;
                }
            }

            IGeoEntities inexp= inExp.EvaluateOn(aux);

            if (inexp == null)
            {
                scope.Errors.Add(new CompilingError(inExp.Location, ErrorCode.Invalid, "Expression"));
                return null;
            }
            return inexp;
        }
    }
}
