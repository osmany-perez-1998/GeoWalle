using System;
using System.Collections.Generic;


namespace Compiling
{
    public class ScopeExecution
    {
        public IDrawer Drawer { get; private set; }

        
         public List<CompilingError> Errors { get; set; }
        public Stack<string> Colors { get; set; }
        public ScopeExecution Father { get; set; }
        Dictionary<string, IGeoEntities> Assigments;
       public Dictionary<string, List<Tuple<string[], Expression>>> Functions { get; private set; }

        public ScopeExecution(List<CompilingError> errors, IDrawer drawer, ScopeExecution father = null )
        {
            Errors = errors;
            Colors = new Stack<string>();
            Assigments = new Dictionary<string, IGeoEntities>();
            Functions = new Dictionary<string, List<Tuple<string[], Expression>>>();
            this.Father = father;
            Drawer = drawer;
        }

        public void AssignFunction(string identifier, string[] args, Expression Body)
        {
            Tuple<string[], Expression> aux = new Tuple<string[], Expression>(args, Body);
            List<Tuple<string[], Expression>> aux1 = new List<Tuple<string[], Expression>>();
            aux1.Add(aux);
            if (!Functions.ContainsKey(identifier))
            {
                Functions.Add(identifier, aux1);
                
            }
            else Functions[identifier].Add(aux); 
        }

        public void AssignVariable(string identifier, IGeoEntities body)
        {
            if (identifier == "Underscore") return;
            Assigments.Add(identifier, body);
        }

        public Tuple<string[], Expression> GetFunction(string identifier, string[] args)
        {
            if(Functions.ContainsKey(identifier))
            foreach (var tuple in Functions[identifier])
            {
                if (tuple.Item1.Length == args.Length) return tuple;
            }

            if (Father != null)
              return Father.GetFunction(identifier, args); ;
            

            return null;
        }

        public IGeoEntities GetVariable(string identifier)
        {
            if (Assigments.ContainsKey(identifier))
                return Assigments[identifier];
             

            if (Father != null)
                return Father.GetVariable(identifier); 


            return null;
        }
    }
}
