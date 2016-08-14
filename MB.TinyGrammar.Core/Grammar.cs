using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MB.TinyGrammar.Core.Helpers;

namespace MB.TinyGrammar.Core
{
    public class Grammar
    {
        protected List<Symbol> _symbols;
        public IList<Symbol> Symbols { get { return _symbols.AsReadOnly(); } }

        protected List<Substitution> _substitutions;
        public IList<Substitution> Substitutions { get { return _substitutions.AsReadOnly(); } }

        public Symbol StartSymbol { get { return _symbols.Count == 0 ? null : _symbols[0]; } }

        public Grammar()
        {
            _symbols = new List<Symbol>();
            _substitutions = new List<Substitution>();
        }

        public void AddSymbol(Symbol symbol)
        {
            if (_symbols.Contains(symbol))
                throw new TinyGrammarException("Symbol already present.");

            _symbols.Add(symbol);
        }

        public void AddSubstitution(Substitution substitution)
        {
            //if (_substitutions.Any(s => s.Symbol == substitution.Symbol && s.Sentence == substitution.Sentence))
            //    throw new TinyGrammarException("Substitution already present.");

            if (!_symbols.Contains(substitution.Symbol))
                AddSymbol(substitution.Symbol);

            _substitutions.Add(substitution);
        }

        public void AddSubstitution(Symbol symbol, Sentence sentence)
        {
            AddSubstitution(new Substitution(symbol, sentence));
        }

        public void AddSubstitution(string symbolName, string sentenceExpression)
        {
            var symbol = Symbols.FirstOrDefault(s => s.Name == symbolName);
            if (symbol == null) symbol = new Symbol(symbolName);

            var substitution = Substitutions.FirstOrDefault(s => s.Sentence.Expression == sentenceExpression);
            var sentence = (substitution == null) ? new Sentence(sentenceExpression) : substitution.Sentence;

            AddSubstitution(symbol, sentence);
        }
        public Sentence ApplyAllSubstitutions()
        {
            if (StartSymbol == null)
                throw new TinyGrammarException("No symbols found.");

            var startSentence = Sentence.FromSymbol(StartSymbol);

            return ApplyAllSubstitutions(startSentence);
        }

        public Sentence ApplyAllSubstitutions(Sentence startSentence)
        {
            var result = new Sentence(startSentence.Expression);

            var namesToSearch = (from s in Symbols select s.Name).ToArray();

            while (true) // warning: possible infinite loop (A->AA generates A, AA, AAAA, ...)
            {
                var prevExpression = result.Expression;
                foreach(var name in result.GetSymbolNames(namesToSearch))
                {
                    var symbol = Symbols.First(s => s.Name == name);
                    result = ApplyRandomSubstitution(result, symbol);
                }

                if (prevExpression == result.Expression) return result;
            }
        }

        public Sentence ApplyRandomSubstitution(Sentence sentence, Symbol symbol)
        {
            var result = new Sentence(sentence.Expression);

            var substitutions = Substitutions.Where(s => s.Symbol == symbol).ToList();
            if (substitutions.Count == 0)
                return result;

            result.ApplySubstitution(substitutions.PickRandom());

            return result;
        }

        public Sentence ApplySubstitution(Sentence startSentence, Substitution substitution)
        {
            var result = new Sentence(startSentence.Expression);

            result.ApplySubstitution(substitution);

            return result;
        }
    }
}
