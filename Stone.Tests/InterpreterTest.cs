using System.IO;
using Stone;
using Stone.AST;
using Stone.Interpreter;
using Stone.Parsers;
using Stone.Tokens;
using Xunit;

namespace Stone.Tests
{
    public class InterpreterTest
    {
        [Fact]
        public void Interpret()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "samples/interpreter.txt");
            Lexer lexer = new Lexer(new FileStream(filePath, FileMode.Open, FileAccess.Read));
            BasicParser basicParser = new BasicParser();
            IEnvironment environment = new BasicEnvironment();

            while (lexer.Peek(0) != Token.EOF)
            {
                ASTree asTree = basicParser.Parse(lexer);

                if (!(asTree is NullStatement))
                {
                    object result = asTree.Eval(environment);

                    System.Console.WriteLine(result);
                }
            }
        }
    }
}