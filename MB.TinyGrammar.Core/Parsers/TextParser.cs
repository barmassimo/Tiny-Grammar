using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.TinyGrammar.Core.Parsers
{
    public class TextParser
    {
        public Grammar GrammarFromText(string content)
        {
            var result = new Grammar();

            content = content.Replace("\r\n", "\n");

            var lines = content.Split('\n');

            foreach (var line in lines)
            {
                var symbolEndPosition = line.IndexOf(":");
                var symbolName = line.Substring(0, symbolEndPosition);
                var sentenceExpression = line.Substring(symbolEndPosition + 1, line.Length - 1 - symbolEndPosition);

                result.AddSubstitution(symbolName, sentenceExpression);
            }

            return result;
        }
    }
}
