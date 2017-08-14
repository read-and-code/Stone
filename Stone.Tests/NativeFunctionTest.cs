using System.IO;
using Stone;
using Stone.AST;
using Stone.Interpreter;
using Stone.Parsers;
using Stone.Tokens;
using Xunit;

namespace Stone.Tests
{
    public class NativeFunctionTest
    {
        [Fact]
        public void Print()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "samples/native_function.st");
            Lexer lexer = new Lexer(new FileStream(filePath, FileMode.Open, FileAccess.Read));
            ClosureParser closureParser = new ClosureParser();
            IEnvironment environment = new NativeMethods().GetEnvironment(new NestedEnvironment());

            while (lexer.Peek(0) != Token.EOF)
            {
                ASTree asTree = closureParser.Parse(lexer);

                if (!(asTree is NullStatement))
                {
                    object result = asTree.Eval(environment);

                    System.Console.WriteLine(result);
                }
            }
        }
    }
}