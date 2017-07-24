using System.IO;
using Stone;
using Xunit;

namespace Stone.Tests
{
    public class LexerTest
    {
        [Fact]
        public void Read()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "samples/lexer.txt");
            Lexer lexer = new Lexer(new FileStream(filePath, FileMode.Open, FileAccess.Read));
            Token token = lexer.Read();

            Assert.True(token.IsIdentifier);
            Assert.Equal("while", token.Text);
        }
    }
}