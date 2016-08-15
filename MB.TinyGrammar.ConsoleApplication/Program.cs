using MB.TinyGrammar.Core;
using MB.TinyGrammar.Core.Parsers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MB.TinyGrammar.ConsoleApplication
{
    class Program
    {
        const string ArgStartExpression = "/S:";

        static void Main(string[] args)
        {
            string startExpression = null;
            string grammarFileName = null;

            foreach (var arg in args)
            {
                if (arg.StartsWith(ArgStartExpression))
                {
                    if (arg == ArgStartExpression)
                    {
                        Usage();
                        return;
                    }

                    if (startExpression != null)
                    {
                        Usage("Error: start expression defined more than once");
                        return;
                    }

                    startExpression = arg.Substring(ArgStartExpression.Length);
                }
                else // grammar file name
                {
                    if (grammarFileName != null)
                    {
                        Usage();
                        return;
                    }

                    grammarFileName = arg;
                }
            }

            if (grammarFileName == null)
            {
                Usage();
                return;
            }

            // input arguments ok: let's generate a sentence

            string text;
            try
            {
                text = File.ReadAllText(grammarFileName);
            }
            catch (Exception ex)
            {
                Usage("File reading error: " + ex.Message);
                return;
            }

            var parser = new TextParser();
            var g = parser.GrammarFromText(text);

            Sentence result;

            if (startExpression!=null)
                result = g.ApplyAllSubstitutions(new Sentence(startExpression));
            else
                result = g.ApplyAllSubstitutions(); // default start symbol

            Console.WriteLine(result.FinalOutput);
        }

        private static void Usage(string errorMessage)
        {
            Usage();
            Console.WriteLine();
            Console.WriteLine(errorMessage);
        }

        private static void Usage()
        {
            var version = FileVersionInfo.GetVersionInfo(@"MB.TinyGrammar.Core.dll").FileVersion;
            var executableName = Path.GetFileName(Assembly.GetExecutingAssembly().CodeBase);
            Console.Out.WriteLine(string.Format("{0} v.{1}", executableName, version));
            Console.Out.WriteLine(string.Format("Usage: {0} [{1}<expression>] <input_file>", executableName, ArgStartExpression));
            Console.Out.WriteLine(string.Format("\t<input_file>: the grammar definition file", ArgStartExpression));
            Console.Out.WriteLine(string.Format("\t{0}<expression>: (opt.) use <expression> as start expression; by default, the first grammar symbol is used", ArgStartExpression));
            Console.Out.WriteLine(string.Format("Example: {0} {1}\"This is {{SUBJECT}}.\" grammarExample.txt", executableName, ArgStartExpression));
        }
    }
}
