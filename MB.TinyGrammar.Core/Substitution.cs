using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.TinyGrammar.Core
{
    public class Substitution
    {
        public Symbol Symbol { get; set; }

        public Sentence Sentence { get; set; }


        public Substitution(Symbol symbol, Sentence sentence)
        {
            Symbol = symbol;
            Sentence = sentence;
        }
    }
}
