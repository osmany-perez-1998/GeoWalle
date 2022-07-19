﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class PointSequenceInput : Statement
    {
        string identifier;
        CodeLocation location;
        public PointSequenceInput(CodeLocation location, string identifier)
        {
            this.identifier = identifier;
            this.location = location;

        }
        public override bool CheckSemantics(ScopeCheckSemantic scope, List<CompilingError> errors)
        {
            if (scope.AlreadyAssignedVariable(identifier))
            {
                errors.Add(new CompilingError(location, ErrorCode.Invalid, "Variable assigment"));
                return false;
            }

            scope.AddVariableDeclaration(identifier);
            return true;
        }

        public override void Execute(ScopeExecution scope, IDrawer drawer)
        {
            Sequence a = new Sequence(new GeoPoint());
            scope.AssignVariable(identifier, a);
        }
    }
}
