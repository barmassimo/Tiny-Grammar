using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.TinyGrammar.Core.Helpers
{
    class SubstitutionHelper
    {
        private const string SymbolStartToken = "{";
        private const string SymbolEndToken = "}";
        private readonly static string UniqueToken = Guid.NewGuid().ToString();

        public string HandleSpecialCharactersForSubstitution(string expression)
        {
            var result = expression;
            result = result.Replace(SymbolStartToken + SymbolStartToken, "CURLY_BRACKET_BEGIN_" + UniqueToken);
            result = result.Replace(SymbolEndToken + SymbolEndToken, "CURLY_BRACKET_END_" + UniqueToken);

            return result;
        }

        public IList<string> GetSymbolNames(string expression, string[] namesToSearch)
        {
            var result = new List<string>();
            foreach (var name in namesToSearch)
            {
                if (expression.Contains(SymbolStartToken + name + SymbolEndToken))
                    result.Add(name);
            }

            return result;
        }

        public string UnHandleSpecialCharactersForSubstitution(string expression)
        {
            var result = expression;
            result = result.Replace("CURLY_BRACKET_BEGIN_" + UniqueToken, SymbolStartToken + SymbolStartToken);
            result = result.Replace("CURLY_BRACKET_END_" + UniqueToken, SymbolEndToken + SymbolEndToken);

            return result;
        }

        public string GetFinalOutput(string expression)
        {
            var result = expression;

            result = result.Replace(SymbolStartToken + SymbolStartToken, SymbolStartToken);
            result = result.Replace(SymbolEndToken + SymbolEndToken, SymbolEndToken);

            return result;
        }

        public string Replace(string originalExpression, string symbolName, string replaceExpression)
        {
            var result = originalExpression;

            result = result.ReplaceFirst(SymbolStartToken + symbolName + SymbolEndToken, replaceExpression);

            return result;
        }
    }
}
