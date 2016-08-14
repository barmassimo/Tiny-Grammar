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

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    continue;

                var symbolEndPosition = line.IndexOf(":");
                var symbolName = line.Substring(0, symbolEndPosition);
                var sentenceExpression = line.Substring(symbolEndPosition + 1, line.Length - 1 - symbolEndPosition);

                result.AddSubstitution(helper.CleanupSymbolName(symbolName), sentenceExpression);
            }

            return result;
        }
    }
}
