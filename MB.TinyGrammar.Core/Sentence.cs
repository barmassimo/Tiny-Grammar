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
                return _helper.GetFinalOutput(_helper.UnHandleSpecialCharacters(Expression));
            }
        }

        public Sentence(string expression)
        {
            _helper = new SubstitutionHelper();

            Expression = _helper.HandleSpecialCharacters(expression);
        }

        public static Sentence FromSymbol(Symbol symbol)
        {
            return new Sentence(SubstitutionHelper.GetExpressionFromSymbolName(symbol.Name));
        }

        public IList<string> GetSymbolNames(string[] namesToSearch)
        {
            return _helper.GetSymbolNames(Expression, namesToSearch);
        }

        public void ApplySubstitution(Substitution substitution)
        {
            Expression = _helper.Replace(Expression, substitution.Symbol.Name, substitution.Sentence.Expression);
        }
    }
}