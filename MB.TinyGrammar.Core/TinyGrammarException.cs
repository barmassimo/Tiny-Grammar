using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.TinyGrammar.Core
{
    [Serializable]
    public class TinyGrammarException : Exception
    {
        public TinyGrammarException() { }
        public TinyGrammarException(string message) : base(message) { }
        public TinyGrammarException(string message, Exception inner) : base(message, inner) { }
        protected TinyGrammarException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
