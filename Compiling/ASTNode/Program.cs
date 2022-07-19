using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class Program : ASTNode
    {
        public Program(List<Statement> Statements)
        {
            this.Statements = Statements;
        }
        public List<Statement> Statements { get; set; }
        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {

            foreach (Statement Statement in Statements)
            {
                if (!Statement.CheckSemantics(scope,errors)) return false;
            }
            return true;
        }

        public void Run(ScopeExecution scope, IDrawer drawer)
        {
            int initial = scope.Errors.Count;
            foreach (var item in Statements)
            {
                item.Execute(scope,drawer);
                if (scope.Errors.Count != initial)
                    break;
                
            }
        }
    }
}
