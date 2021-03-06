using System;
using System.IO;
using Stone.AST;
using Stone.Parsers;
using Stone.Tokens;
using Xunit;

namespace Stone.Tests
{
    public class BasicParserTest
    {
        [Fact]
        public void Parse()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "samples/lexer2.st");
            Lexer lexer = new Lexer(new FileStream(filePath, FileMode.Open, FileAccess.Read));
            BasicParser basicParser = new BasicParser();

            while (lexer.Peek(0) != Token.EOF)
            {
                ASTNode astNode = basicParser.Parse(lexer);

                Console.WriteLine(astNode.ToString());
            }
        }
    }
}