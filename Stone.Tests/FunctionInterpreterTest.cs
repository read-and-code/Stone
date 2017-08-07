using System.IO;
using Stone;
using Stone.AST;
using Stone.Interpreter;
using Stone.Parsers;
using Stone.Tokens;
using Xunit;

namespace Stone.Tests
{
    public class FunctionInterpreterTest
    {
        [Fact]
        public void Interpret()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "samples/sum.txt");
            Lexer lexer = new Lexer(new FileStream(filePath, FileMode.Open, FileAccess.Read));
            FunctionParser functionParser = new FunctionParser();
            IEnvironment environment = new NestedEnvironment();

            while (lexer.Peek(0) != Token.EOF)
            {
                ASTree asTree = functionParser.Parse(lexer);

                if (!(asTree is NullStatement))
                {
                    object result = asTree.Eval(environment);

                    System.Console.WriteLine(result);
                }
            }
        }
    }
}