using System.IO;
using Stone;
using Stone.AST;
using Stone.Interpreter;
using Stone.Parsers;
using Stone.Tokens;
using Xunit;

namespace Stone.Tests
{
    public class ClosureInterpreterTest
    {
        [Fact]
        public void Counter()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "samples/closure.st");
            Lexer lexer = new Lexer(new FileStream(filePath, FileMode.Open, FileAccess.Read));
            ClosureParser closureParser = new ClosureParser();
            IEnvironment environment = new NestedEnvironment();

            while (lexer.Peek(0) != Token.EOF)
            {
                ASTNode astNode = closureParser.Parse(lexer);

                if (!(astNode is NullStatement))
                {
                    object result = astNode.Eval(environment);

                    System.Console.WriteLine(result);
                }
            }
        }
    }
}