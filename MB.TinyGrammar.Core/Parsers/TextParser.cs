using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MB.TinyGrammar.Core.Helpers;

namespace MB.TinyGrammar.Core.Parsers
{
    public class TextParser
    {
        public Grammar GrammarFromText(string content)
        {
            var result = new Grammar();

            var helper = new SubstitutionHelper();

            content = content.Replace("\r\n", "\n");

            var lines = content.Split('\n');

            var lineCount = 0;

            foreach (var line in lines)
            {
                lineCount++;

                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    continue;

                if (!line.Contains(":"))
                    throw new TinyGrammarException(string.Format("Error on line {0}: missing \":\".", lineCount));

                var symbolEndPosition = line.IndexOf(":");
                var symbolName = line.Substring(0, symbolEndPosition);
                var sentenceExpression = line.Substring(symbolEndPosition + 1, line.Length - 1 - symbolEndPosition);

                sentenceExpression = helper.HandleSpecialCharacters(sentenceExpression);

                foreach (var altSentenceExpression in helper.GetAlternativeExpressions(sentenceExpression))
                {
                    result.AddSubstitution(
                        helper.CleanupSymbolName(symbolName), 
                        helper.UnHandleSpecialCharacters(altSentenceExpression)
                       );
                }

                
            }

            return result;
        }
    }
}
