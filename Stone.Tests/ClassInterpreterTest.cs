using System.IO;
using Stone;
using Stone.AST;
using Stone.Interpreter;
using Stone.Parsers;
using Stone.Tokens;
using Xunit;

namespace Stone.Tests
{
    public class ClassInterpreterTest
    {
        [Fact]
        public void CreateObject()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "samples/class.st");
            Lexer lexer = new Lexer(new FileStream(filePath, FileMode.Open, FileAccess.Read));
            ClassParser classParser = new ClassParser();
            IEnvironment environment = new NativeMethods().GetEnvironment(new NestedEnvironment());

            while (lexer.Peek(0) != Token.EOF)
            {
                ASTree asTree = classParser.Parse(lexer);

                if (!(asTree is NullStatement))
                {
                    object result = asTree.Eval(environment);

                    System.Console.WriteLine(result);
                }
            }
        }
    }
}