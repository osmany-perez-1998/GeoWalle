using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiling.Parser.IControlParser.IStatementParsers
{
    public class ColorParser : IStatementParser
    {
        public ColorParser(KeyParser parser)
        {
            Register(parser);
        }
        public Statement Parse(KeyParser parser, Token token, List<CompilingError> errors)
        {

            string[] colores = { "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", "White", "Black", "Gray" };

            string colorToReturn = string.Empty;
            if (!parser.MatchValue(TokenValues.Color)) return null;

            for (int i = 0; i < colores.Length; i++)
            {
                if (parser.LookFromIndex().Value == colores[i])
                {
                    colorToReturn = colores[i];
                    break;
                }
            }
            if (colorToReturn == string.Empty)
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "A color name"));
                return null;
            }
            if (parser.MatchValue(colorToReturn))
            {
                if (!parser.MatchValue(TokenValues.StatementSeparator))
                {
                    errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Statement separator"));
                    return null;
                }
            }
            
                return new ColorNode(token.Location,colorToReturn);
          

        }

        public void Register(KeyParser parser)
        {
            parser.RegisterIStatementParserByValue(TokenValues.Color, this);
        }
    }
}
