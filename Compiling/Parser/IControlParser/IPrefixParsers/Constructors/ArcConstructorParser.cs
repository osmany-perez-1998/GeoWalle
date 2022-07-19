using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling
{
    public class ArcConstructorParser : IPrefixParser
    {
        public ArcConstructorParser(KeyParser parser)
        {
            Register(parser);
        }
        public Expression Parse(KeyParser parser, Token token, List<CompilingError> errors)
        {
            Expression p1;
            Expression p2;
            Expression p3;
            Expression measure;

            // if (parser.MatchValue(TokenValues.Arc))
            if (!parser.MatchValue(TokenValues.OpenBracket))
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Open Bracket"));
                return null;
            }
            {
                p1 = parser.ParseExpression(errors);
                if (p1 == null)
                {
                    errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Expression"));
                    return null;
                }
                if (!parser.MatchValue(TokenValues.ValueSeparator))
                {
                    errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Coma"));
                    return null;
                }
                {
                    p2 = parser.ParseExpression(errors);
                    if (p2 == null)
                    {
                        errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Expression"));
                        return null;
                    }
                    if (!parser.MatchValue(TokenValues.ValueSeparator))
                    {
                        errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Coma"));
                        return null;
                    }
                    {
                        p3 = parser.ParseExpression(errors);
                        if (p3 == null)
                        {
                            errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Expression"));
                            return null;
                        }
                        if (!parser.MatchValue(TokenValues.ValueSeparator))
                        {
                            errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Coma"));
                            return null;
                        }
                        {
                            measure = parser.ParseExpression(errors);
                            if (measure == null)
                            {
                                errors.Add(new CompilingError(token.Location, ErrorCode.Invalid, "Expression"));
                                return null;
                            }
                            if (!parser.MatchValue(TokenValues.ClosedBracket))
                            {
                                errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Closed Bracket"));
                                return null;
                            }
                            {
                                return new ArcConstructorNode(token.Location,p1, p2, p3, measure);
                            }
                        }
                    }
                }
            }
            
        }

        public void Register(KeyParser parser)
        {
            parser.RegisterPrefixParserByValue(TokenValues.Arc, this);
        }
    }
}
