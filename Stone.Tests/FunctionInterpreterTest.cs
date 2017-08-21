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
        public void Sum()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "samples/sum.st");
            Lexer lexer = new Lexer(new FileStream(filePath, FileMode.Open, FileAccess.Read));
            FunctionParser functionParser = new FunctionParser();
            IEnvironment environment = new NestedEnvironment();

            while (lexer.Peek(0) != Token.EOF)
            {
                ASTNode astNode = functionParser.Parse(lexer);

                if (!(astNode is NullStatement))
                {
                    object result = astNode.Eval(environment);

                    System.Console.WriteLine(result);
                }
            }
        }

        [Fact]
        public void Fib()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "samples/fib.st");
            Lexer lexer = new Lexer(new FileStream(filePath, FileMode.Open, FileAccess.Read));
            FunctionParser functionParser = new FunctionParser();
            IEnvironment environment = new NestedEnvironment();

            while (lexer.Peek(0) != Token.EOF)
            {
                ASTNode astNode = functionParser.Parse(lexer);

                if (!(astNode is NullStatement))
                {
                    object result = astNode.Eval(environment);

                    System.Console.WriteLine(result);
                }
            }
        }
    }
}