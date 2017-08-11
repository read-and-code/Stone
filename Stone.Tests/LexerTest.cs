using System.IO;
using Stone;
using Stone.Tokens;
using Xunit;

namespace Stone.Tests
{
    public class LexerTest
    {
        [Fact]
        public void ReadToken()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "samples/lexer.st");
            Lexer lexer = new Lexer(new FileStream(filePath, FileMode.Open, FileAccess.Read));
            Token token = lexer.Read();

            while (token != Token.EOF)
            {
                System.Console.WriteLine(token.ToString());

                token = lexer.Read();
            }
        }
    }
}