using System.Collections.Generic;
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
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "samples/lexer_assignment.st");
            Lexer lexer = new Lexer(new FileStream(filePath, FileMode.Open, FileAccess.Read));
            Token token = lexer.Read();
            List<string> expectedTokens = new List<string>();

            while (token != Token.EOF)
            {
                System.Console.WriteLine(token.ToString());

                if (token.IsIdentifier || token.IsString)
                {
                    expectedTokens.Add(token.Text);
                }
                else if (token.IsNumber)
                {
                    expectedTokens.Add(token.Number.ToString());
                }

                token = lexer.Read();
            }

            Assert.Equal(new List<string> { "i", "=", "1", "\\n" }, expectedTokens);
        }
    }
}