using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.TinyGrammar.Core.Helpers
{
    class SubstitutionHelper
    {
        private const string SymbolStartToken = @"{";
        private const string SymbolEndToken = @"}";
        private const string SymbolVerticalBar = @"|";
        private const string SymbolNewLine = @"\n";

        private readonly static string UniqueToken = Guid.NewGuid().ToString();

        public static string GetExpressionFromSymbolName(string symbolName)
        {
            return SymbolStartToken + symbolName + SymbolEndToken;
        }

        public string CleanupSymbolName(string symbolName)
        {
            return symbolName.Replace(SymbolStartToken, string.Empty).Replace(SymbolEndToken, string.Empty);
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

        public string HandleSpecialCharacters(string expression)
        {
            var result = expression;
            result = result.Replace(SymbolStartToken + SymbolStartToken, "BRACE_BEGIN_" + UniqueToken);
            result = result.Replace(SymbolEndToken + SymbolEndToken, "BRACE_END_" + UniqueToken);
            result = result.Replace(SymbolVerticalBar + SymbolVerticalBar, "VERTICAL_BAR_" + UniqueToken);

            return result;
        }

        public string UnHandleSpecialCharacters(string expression)
        {
            var result = expression;
            result = result.Replace("BRACE_BEGIN_" + UniqueToken, SymbolStartToken + SymbolStartToken);
            result = result.Replace("BRACE_END_" + UniqueToken, SymbolEndToken + SymbolEndToken);
            result = result.Replace("VERTICAL_BAR_" + UniqueToken, SymbolVerticalBar + SymbolVerticalBar);

            return result;
        }

        public string GetFinalOutput(string expression)
        {
            var result = expression;

            result = result.Replace(SymbolStartToken + SymbolStartToken, SymbolStartToken);
            result = result.Replace(SymbolEndToken + SymbolEndToken, SymbolEndToken);
            result = result.Replace(SymbolVerticalBar + SymbolVerticalBar, SymbolVerticalBar);
            result = result.Replace(SymbolNewLine, "\n");

            return result;
        }

        public string Replace(string originalExpression, string symbolName, string replaceExpression)
        {
            var result = originalExpression;

            result = result.ReplaceFirst(SymbolStartToken + symbolName + SymbolEndToken, replaceExpression);

            return result;
        }

        public IEnumerable<string> GetAlternativeExpressions(string expression)
        {
            return expression.Split(SymbolVerticalBar[0]);
        }
    }
}
