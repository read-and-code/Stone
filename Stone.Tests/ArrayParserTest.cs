using System.IO;
using Stone;
using Stone.AST;
using Stone.Interpreter;
using Stone.Parsers;
using Stone.Tokens;
using Xunit;

namespace Stone.Tests
{
    public class ArrayParserTest
    {
        [Fact]
        public void CreateArray()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "samples/array.st");
            Lexer lexer = new Lexer(new FileStream(filePath, FileMode.Open, FileAccess.Read));
            ArrayParser arrayParser = new ArrayParser();
            IEnvironment environment = new NativeMethods().GetEnvironment(new NestedEnvironment());

            while (lexer.Peek(0) != Token.EOF)
            {
                ASTree asTree = arrayParser.Parse(lexer);

                if (!(asTree is NullStatement))
                {
                    object result = asTree.Eval(environment);

                    System.Console.WriteLine(result);
                }
            }
        }
    }
}