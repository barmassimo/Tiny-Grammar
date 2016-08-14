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

        public IList<string> GetSymbolNames(string[] namesToSearch)
        {

            BeginSubstitution();
            var result = _helper.GetSymbolNames(Expression, namesToSearch);
            EndSubstitution();

            return result;
        }

        public void ApplySubstitution(Substitution substitution)
        {
            BeginSubstitution();
            Expression = _helper.Replace(Expression, substitution.Symbol.Name, substitution.Sentence.Expression);
            EndSubstitution();
        }

        private void BeginSubstitution()
        {
            Expression = _helper.HandleSpecialCharactersForSubstitution(Expression);
        }

        private void EndSubstitution()
        {
            Expression = _helper.UnHandleSpecialCharactersForSubstitution(Expression);
        }
    }
}