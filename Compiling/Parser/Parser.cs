using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace Compiling
{
    public class KeyParser
    {

        Token[] tokens;
         int index;
        //  List<string> operations;
        Dictionary<TokenType, IPrefixParser> prefixParsersType = new Dictionary<TokenType, IPrefixParser>();
        Dictionary<TokenType, IInfixParser> infixParsersType = new Dictionary<TokenType, IInfixParser>();
        Dictionary<TokenType, List<IStatementParser>> statementParsersType = new Dictionary<TokenType, List<IStatementParser>>();
        Dictionary<string, IPrefixParser> prefixParsersValue = new Dictionary<string, IPrefixParser>();
        Dictionary<string, IInfixParser> infixParsersValue = new Dictionary<string, IInfixParser>();
        Dictionary<string, IStatementParser> statementParsersValue = new Dictionary<string, IStatementParser>();



        public KeyParser(IEnumerable<Token> token)
        {
            tokens = token.ToArray();
            index = 0;
            string directory = Directory.GetCurrentDirectory();
            foreach (var file in Directory.GetFiles(directory))
            {
                if (Path.GetExtension(file) != ".exe" && Path.GetExtension(file) != ".dll")
                    continue;
                var library = Assembly.LoadFile(file);
                foreach (var type in library.GetTypes())
                {
                    if (type.IsClass && !type.IsAbstract && typeof(IControlParser).IsAssignableFrom(type))
                        Activator.CreateInstance(type, this);
                }
            }

        }

         public Program ParserProgram(List<CompilingError> errors)
        {
            List<Statement> toReturn = new List<Statement>();
            int initial = errors.Count;

            while (index < tokens.Length - 1)
            {
                
                Statement Aux = ParseStatement(errors);
                if (initial != errors.Count) break;
                toReturn.Add(Aux);
            }
            return new Program(toReturn);
        }
        #region Expression
        public Expression ParseExpression( List<CompilingError> errors, int Precedence = 0)
        {
            
            Token token = Consume();
            IPrefixParser prefix;

            prefix = PrefixParser(token);
            if (prefix == null)
            {
                errors.Add(new CompilingError(token.Location, ErrorCode.Expected, "Expression"));
                return null;
            }

            Expression left = prefix.Parse(this, token, errors);

         

            while (Precedence < GetPrecedence())
            {
                token = Consume();
                IInfixParser infix;
                if (infixParsersType.TryGetValue(token.Type, out infix) || infixParsersValue.TryGetValue(token.Value, out infix))
                {
                    left = infix.Parse(this, left, token,errors);
                }
            }
            return left;

        }
        #endregion
        public Token Consume()
        {
            if (index >= tokens.Length)
                return null;
            return tokens[index++];
        }
        #region Statement
        public Statement ParseStatement(List<CompilingError> errors)
        {
            int initial = errors.Count;
            Statement ToReturn = null;
            foreach (var stat in statementParsersValue)
            {
                ToReturn = stat.Value.Parse(this, tokens[index],errors);
                if (ToReturn != null || errors.Count>initial)
                {
                    return ToReturn;
                }
            }

            foreach (var item in statementParsersType)
            {
                foreach (var item1 in item.Value)
                {
                    ToReturn = item1.Parse(this, tokens[index],errors);
                    if (ToReturn != null || errors.Count>initial)
                    {
                        return ToReturn;
                    }
                }
            }

            errors.Add(new CompilingError(tokens[index].Location, ErrorCode.Expected, "Statement"));
            return ToReturn;
        }


        #endregion

        public IPrefixParser PrefixParser(Token value)
        {
            IPrefixParser ToReturn;
            if (prefixParsersValue.TryGetValue(value.Value, out ToReturn) || prefixParsersType.TryGetValue(value.Type, out ToReturn))
                return ToReturn;
            return null;
        }

        public int GetPrecedence()
        {
          
            Token token = LookFromIndex();
            if (token == null) return -1;
            IInfixParser parser;
            if (infixParsersType.TryGetValue(token.Type, out parser) || infixParsersValue.TryGetValue(token.Value, out parser))
            {
                return parser.GetPrecedence();
            }
            return -1;
        }

        public Token LookFromIndex(int AddToindex = 0)
        {
            if (index + AddToindex >= tokens.Length) return null;
           return tokens[index + AddToindex];
        }

        internal bool MatchType(TokenType Type)
        {
            if (index == tokens.Length) return false;

            if (tokens[index].Type == Type) { index++; return true; }

            return false;
        }

        internal bool MatchValue(string value)
        {
            if (index == tokens.Length) return false;

            if (tokens[index].Value == value) { index++; return true; }

            return false;
        }

        public void RegisterInfixParserByValue(string Operation, IInfixParser ToSave)
        {
            infixParsersValue.Add(Operation, ToSave);
        }
        public void RegisterPrefixParserByValue(string Operation, IPrefixParser ToSave)
        {
            prefixParsersValue.Add(Operation, ToSave);
        }
        public void RegisterInfixParserByType(TokenType Operation, IInfixParser ToSave)
        {
            infixParsersType.Add(Operation, ToSave);
        }
        public void RegisterPrefixParserByType(TokenType Operation, IPrefixParser ToSave)
        {
            prefixParsersType.Add(Operation, ToSave);
        }
        public void RegisterIStatementParserByValue(string Operation, IStatementParser ToSave)
        {
            statementParsersValue.Add(Operation, ToSave);
        }
        public void RegisterIStatementParserByType(TokenType Operation, IStatementParser ToSave)
        {
            if (statementParsersType.ContainsKey(Operation))
                statementParsersType[Operation].Add(ToSave);
            else
            {
                List<IStatementParser> a = new List<IStatementParser>();
                a.Add(ToSave);
                statementParsersType.Add(Operation, a);

            }
        }


    }
}
