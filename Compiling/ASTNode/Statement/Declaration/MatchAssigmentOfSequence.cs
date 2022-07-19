using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling

{
    public class MatchAssigmentOfSequence : Statement
    {
        string[] VarNames;
        Expression body;
        CodeLocation location;

        public MatchAssigmentOfSequence(CodeLocation location, string[] VarNames, Expression body)
        {
            this.VarNames = VarNames;
            this.body = body;
            this.location = location;
        }
        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {
            foreach (var variable in VarNames)
            {
                if (scope.AlreadyAssignedVariable(variable))
                {
                    errors.Add(new CompilingError(location, ErrorCode.Invalid, "Variable assigment"));
                    return false;
                }
                if(variable!="Underscore")scope.AddVariableDeclaration(variable);
            }

            bool bod= body.CheckSemantics(scope,errors);
            if (!bod)
            {
                errors.Add(new CompilingError(location, ErrorCode.Invalid, "Expression"));
                return false;
            }
            return true;
        }

        public override void Execute(ScopeExecution scope, IDrawer drawer)
        {
            IGeoEntities a = body.EvaluateOn(scope);

           
            if(a as Sequence==null && a as Undefined == null)
            {
                scope.Errors.Add(new CompilingError(body.Location, ErrorCode.Invalid, "Expression for match assigments"));
                return;
            }
            Sequence aux = a as Sequence;
            Undefined aux1 = a as Undefined;
            if (aux1 != null)
            {
                for (int i = 0; i < VarNames.Length; i++)
                {
                    scope.AssignVariable(VarNames[i], new Undefined());
                }
                return;
            }
            Queue<IGeoEntities> toAssign = new Queue<IGeoEntities>();
            if(aux!=null)
            if (aux.FiniteSequence || aux.NumberIntervalSequence)
            {
                foreach (var item in aux.FiniteStorage)
                {
                    toAssign.Enqueue(item);
                };
            }

         
            #region FiniteSequence
            if (aux.FiniteSequence)
            {

                for (int i = 0; i < VarNames.Length; i++)
                {
                    if (VarNames[i] == "Underscore")
                    {
                        if(toAssign.Count!=0)
                           toAssign.Dequeue();
                        continue;
                    }
                    if (i == VarNames.Length - 1 && aux.FiniteStorage.Count<IGeoEntities>() > VarNames.Length)
                    {             

                        scope.AssignVariable(VarNames[i], new Sequence(toAssign.ToList<IGeoEntities>()));
                        toAssign.Clear();
                        break;
                    }
                    if (toAssign.Count() != 0)
                    {


                        scope.AssignVariable(VarNames[i], toAssign.Dequeue());
                    }
                    else if (i < VarNames.Length - 1)
                    {

                        scope.AssignVariable(VarNames[i], new Undefined());
                    }
                    else
                    {

                        scope.AssignVariable(VarNames[i], new Sequence());
                    }
                }
            }
            #endregion

            #region NumberIntervalSequence
            if (aux.NumberIntervalSequence)
            {
                for (int i = 0; i < VarNames.Length; i++)
                {
                    if (VarNames[i] == "Underscore")
                    {
                        toAssign.Dequeue();
                        continue;
                    }
                    if (i == VarNames.Length - 1 && aux.FiniteStorage.Count<IGeoEntities>() > VarNames.Length)
                    {
                        //   if (VarNames[i] == "Underscore") continue;

                        scope.AssignVariable(VarNames[i], new Sequence((Number)toAssign.Dequeue(), (Number)toAssign.Last()));

                    }
                    if (toAssign.Count() != 0)
                    {
                        // if (VarNames[i] == "Underscore") continue;
                        scope.AssignVariable(VarNames[i], toAssign.Dequeue());
                    }
                    else if (i < VarNames.Length - 1)
                    {
                        //  if (VarNames[i] == "Underscore") continue;
                        scope.AssignVariable(VarNames[i], new Undefined());
                    }
                    else
                    {
                        //  if (VarNames[i] == "Underscore") continue;
                        scope.AssignVariable(VarNames[i], new Sequence());
                    }

                }
            }
            #endregion

            #region GeoEntityInifiniteSequence

            if (aux.GeoentityInfiniteSequence)
            {
                var current = aux.InfiniteStorage.GetEnumerator();
                for (int i = 0; i < VarNames.Length; i++)
                {
                    if (VarNames[i] == "Underscore")
                    {

                        continue;
                    }
                    if (current.MoveNext() && i != VarNames.Length - 1)
                    {
                        scope.AssignVariable(VarNames[i], current.Current);
                    }
                    else
                    {
                        if (aux.InsideType == "point")
                            scope.AssignVariable(VarNames[i], new Sequence(new GeoPoint()));
                        if (aux.InsideType == "circle")
                            scope.AssignVariable(VarNames[i], new Sequence(new Circle(new GeoPoint(), new Measure())));
                        if (aux.InsideType == "line")
                            scope.AssignVariable(VarNames[i], new Sequence(new Line(new GeoPoint(), new GeoPoint())));
                        if (aux.InsideType == "ray")
                            scope.AssignVariable(VarNames[i], new Sequence(new Ray(new GeoPoint(), new GeoPoint())));
                        if (aux.InsideType == "segment")
                            scope.AssignVariable(VarNames[i], new Sequence(new Segment(new GeoPoint(), new GeoPoint())));
                        if (aux.InsideType == "arc")
                            scope.AssignVariable(VarNames[i], new Sequence(new Arc(new GeoPoint(), new GeoPoint(), new GeoPoint(), new Measure())));
                    }
                }
            }
            #endregion

            #region InfiniteNumberSequence

            if (aux.NumberInfiniteSequence)
            {
                var current = aux.InfiniteStorage.GetEnumerator();
                for (int i = 0; i < VarNames.Length; i++)
                {

                    if (VarNames[i] == "Underscore")
                    {
                        //aux.InfiniteStorage.GetEnumerator().MoveNext();
                        current.MoveNext();
                        continue;
                    }
                    if (i == VarNames.Length - 1)
                    {
                        // if (VarNames[i] == "Underscore") continue;
                        current.MoveNext();
                        scope.AssignVariable(VarNames[i], new Sequence((Number)current.Current));
                        break;
                    }
                    if (current.MoveNext())
                    {
                        // if (VarNames[i] == "Underscore") continue;
                        scope.AssignVariable(VarNames[i], current.Current);
                    }
                    //else if (i < VarNames.Length - 1)
                    //{
                    //    // if (VarNames[i] == "Underscore") continue;
                    //    scope.AssignVariable(VarNames[i], new Undefined());
                    //}
                    //else
                    //{
                    //    // if (VarNames[i] == "") continue;
                    //    scope.AssignVariable(VarNames[i], new Sequence());
                    //}
                }
            }
            #endregion


        }
    }
}
