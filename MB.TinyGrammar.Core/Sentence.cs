using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MB.TinyGrammar.Core.Helpers;

namespace MB.TinyGrammar.Core
{
    public class Sentence
    {
        private SubstitutionHelper _helper;

        public string Expression { get; set; }

        public string FinalOutput { get
            {
                return _helper.GetFinalOutput(Expression);
            }
        }

        public Sentence(string expression)
        {
            _helper = new SubstitutionHelper();

            Expression = expression;
        }

        public static Sentence FromSymbol(Symbol symbol)
        {
            return new Sentence(SubstitutionHelper.GetExpressionFromSymbolName(symbol.Name));
        }

        public IList<string> GetSymbolNames(string[] namesToSearch)
        {
            BeginSpecialCharactersHandling();
            var result = _helper.GetSymbolNames(Expression, namesToSearch);
            EndSpecialCharactersHandling();

            return result;
        }

        public void ApplySubstitution(Substitution substitution)
        {
            BeginSpecialCharactersHandling();
            Expression = _helper.Replace(Expression, substitution.Symbol.Name, substitution.Sentence.Expression);
            EndSpecialCharactersHandling();
        }

        private void BeginSpecialCharactersHandling()
        {
            Expression = _helper.HandleSpecialCharacters(Expression);
        }

        private void EndSpecialCharactersHandling()
        {
            Expression = _helper.UnHandleSpecialCharacters(Expression);
        }
    }
}