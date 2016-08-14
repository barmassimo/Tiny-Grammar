using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using MB.TinyGrammar.Core;
using MB.TinyGrammar.Core.Parsers;
using System.IO;

namespace MB.TinyGrammar.Core.Test
{
    public class Tests
    {
        [Fact]
        public void AddSymbol()
        {
            var g = new Grammar();
            Assert.Equal(0, g.Symbols.Count);

            var subject = new Symbol("SUBJECT");
            g.AddSymbol(subject);

            Assert.Equal(1, g.Symbols.Count);
        }

        [Fact]
        public void AddExistingSymbolFails()
        {
            var g = new Grammar();

            var subject = new Symbol("SUBJECT");
            g.AddSymbol(subject);

            Exception ex = Assert.Throws<TinyGrammarException>(() => g.AddSymbol(subject));
            Assert.Equal("Symbol already present.", ex.Message);

            Assert.Equal(1, g.Symbols.Count);
        }

        [Fact]
        public void AddSubstitution()
        {
            var g = new Grammar();

            var subject = new Symbol("SUBJECT");
            g.AddSubstitution(subject, new Sentence("John"));

            Assert.Equal(1, g.Symbols.Count);
            Assert.Equal(1, g.Substitutions.Count);
            Assert.Equal(subject, g.Substitutions[0].Symbol);
        }

        [Fact]
        public void AddExistingSubstitutionFails()
        {
            var g = new Grammar();

            var subject = new Symbol("SUBJECT");
            var sentence = new Sentence("John");

            g.AddSubstitution(new Substitution(subject, sentence));

            Exception ex = Assert.Throws<TinyGrammarException>(() => g.AddSubstitution(subject, sentence));
            Assert.Equal("Substitution already present.", ex.Message);

            Assert.Equal(1, g.Symbols.Count);
            Assert.Equal(1, g.Substitutions.Count);
            Assert.Equal(subject, g.Substitutions[0].Symbol);
        }

        [Fact]
        public void SimpleGeneration()
        {
            var g = new Grammar();

            var subject = new Symbol("SUBJECT");

            var substitution = new Substitution(subject, new Sentence("John"));
            g.AddSubstitution(substitution);

            var startSentence = new Sentence("{SUBJECT} is a subject.");

            var result = g.ApplySubstitution(startSentence, substitution);

            Assert.Equal("John is a subject.", result.FinalOutput);
        }

        [Fact]
        public void SimpleGenerationWithBracket()
        {
            var g = new Grammar();

            var subject = new Symbol("SUBJECT");

            var substitution = new Substitution(subject, new Sentence("John"));
            g.AddSubstitution(substitution);

            var startSentence = new Sentence("{SUBJECT} is a subject {{bracket test: {{SUBJECT}}, {{HELLO!}}}}.");

            var result = g.ApplySubstitution(startSentence, substitution);

            Assert.Equal("John is a subject {bracket test: {SUBJECT}, {HELLO!}}.", result.FinalOutput);
        }

        [Fact]
        public void GenerationWithMultipleOption()
        {
            var g = new Grammar();

            var subject = new Symbol("SUBJECT");
            var animal = new Symbol("ANIMAL");
            var person = new Symbol("PERSON");

            g.AddSubstitution(new Substitution(subject, new Sentence("{ANIMAL}")));
            g.AddSubstitution(new Substitution(subject, new Sentence("{PERSON}")));
            g.AddSubstitution(new Substitution(animal, new Sentence("a dog")));
            g.AddSubstitution(new Substitution(animal, new Sentence("a cat")));
            g.AddSubstitution(new Substitution(person, new Sentence("John")));

            var startSentence = new Sentence("This is {SUBJECT}.");

            var results = new string[100];
            for (int i = 0; i < 100; i++)
            {
                var result = g.ApplyRandomSubstitution(startSentence, subject);
                result = g.ApplyRandomSubstitution(result, animal);
                result = g.ApplyRandomSubstitution(result, person);

                results[i] = result.FinalOutput;
            }

            Assert.True(results.Contains("This is a dog."));
            Assert.True(results.Contains("This is a cat."));
            Assert.True(results.Contains("This is John."));
        }

        [Fact]
        public void GenerationWithAllSubstitutions()
        {
            var g = new Grammar();

            var subject = new Symbol("SUBJECT");
            var animal = new Symbol("ANIMAL");
            var person = new Symbol("PERSON");

            g.AddSubstitution(new Substitution(subject, new Sentence("{ANIMAL}")));
            g.AddSubstitution(new Substitution(subject, new Sentence("{PERSON}")));
            g.AddSubstitution(new Substitution(animal, new Sentence("a dog")));
            g.AddSubstitution(new Substitution(animal, new Sentence("a cat")));
            g.AddSubstitution(new Substitution(person, new Sentence("John")));

            var startSentence = new Sentence("This is {SUBJECT}.");

            var results = new string[100];
            for (int i = 0; i < 100; i++)
            {
                var result = g.ApplyAllSubstitutions(startSentence);
                results[i] = result.FinalOutput;
            }

            Assert.True(results.Contains("This is a dog."));
            Assert.True(results.Contains("This is a cat."));
            Assert.True(results.Contains("This is John."));
        }

        [Fact]
        public void ParseFromFile()
        {
            var parser = new TextParser();

            var text = File.ReadAllText(@"grammarExample.txt");

            var g = parser.GrammarFromText(text);

            Assert.Equal("SUBJECT", g.StartSymbol.Name);

            Assert.Equal(3, g.Symbols.Count);
            Assert.Equal(g.Symbols[0].Name, "SUBJECT");
            Assert.Equal(g.Symbols[1].Name, "ANIMAL");
            Assert.Equal(g.Symbols[2].Name, "PERSON");

            Assert.Equal(5, g.Substitutions.Count);
            Assert.Equal(g.Substitutions[0].Symbol, g.Symbols[0]);
            Assert.Equal(g.Substitutions[1].Symbol, g.Symbols[0]);
            Assert.Equal(g.Substitutions[2].Symbol, g.Symbols[1]);
            Assert.Equal(g.Substitutions[3].Symbol, g.Symbols[1]);
            Assert.Equal(g.Substitutions[4].Symbol, g.Symbols[2]);
            Assert.Equal(g.Substitutions[0].Sentence.Expression, "{ANIMAL}");
            Assert.Equal(g.Substitutions[1].Sentence.Expression, "{PERSON}");
            Assert.Equal(g.Substitutions[2].Sentence.Expression, "a dog");
            Assert.Equal(g.Substitutions[3].Sentence.Expression, "a cat");
            Assert.Equal(g.Substitutions[4].Sentence.Expression, "John");


        }
    }
}
