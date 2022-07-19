using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class ScopeCheckSemantic
    {

        public ScopeCheckSemantic Father { get; set; }
        // public Dictionary<string, Type> VariableDeclarationCheckSemantic { get; set; }
        public List<string> VariableDeclarationCheckSemantic { get; set; }
        public Dictionary<string, List<int>> FunctionsCheckSemantic { get; set; }


        public ScopeCheckSemantic(ScopeCheckSemantic father = null)
        {

            this.Father = father;
            VariableDeclarationCheckSemantic = new List<string>();
            FunctionsCheckSemantic = new Dictionary<string, List<int>>();
        }


        public bool AlreadyAssignedFunction(string toAssign, string[] args)
        {
            foreach (var item in FunctionsCheckSemantic)
            {
                if (item.Key == toAssign)
                {
                    foreach (var count in item.Value)
                    {
                        if (count == args.Length) return true;
                    }
                }
            }

            if (Father != null && Father.AlreadyAssignedFunction(toAssign, args)) return true;
            return false;
        }
        public bool AlreadyAssignedVariable(string ToAssign)
        {
            foreach (var item in VariableDeclarationCheckSemantic)
            {
                if (item == ToAssign) return true;
            }
            if (Father != null && Father.AlreadyAssignedVariable(ToAssign)) return true;
            return false;
        }

        public void AddVariableDeclaration(string ToAssign)
        {
            VariableDeclarationCheckSemantic.Add(ToAssign);

        }

        public void AddFunctionDeclaration(string ToAssign, string[] Params)
        {
            if (FunctionsCheckSemantic.ContainsKey(ToAssign))
                FunctionsCheckSemantic[ToAssign].Add(Params.Length);

            else
            {
                List<int> count = new List<int>();
                count.Add(Params.Length);
                FunctionsCheckSemantic.Add(ToAssign,count);
            }
        }


    }
}
