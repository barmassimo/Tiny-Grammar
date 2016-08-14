using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.TinyGrammar.Core
{
    public class Symbol
    {
        public string Name { get; set; }

        public Symbol(string name)
        {
            Name = name;
        }
    }
}
