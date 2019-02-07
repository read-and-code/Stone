using System;
using System.IO;
using Stone.AST;
using Stone.Interpreter;
using Stone.Parsers;
using Stone.Tokens;
using Xunit;

namespace Stone.Tests
{
    public class InterpreterTest
    {
        [Fact]
        public void Interpret()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "samples/interpreter.st");
            Lexer lexer = new Lexer(new FileStream(filePath, FileMode.Open, FileAccess.Read));
            BasicParser basicParser = new BasicParser();
            IEnvironment environment = new Interpreter.Environment();

            while (lexer.Peek(0) != Token.EOF)
            {
                ASTNode astNode = basicParser.Parse(lexer);

                if (!(astNode is NullStatement))
                {
                    astNode.Lookup(environment.SymbolTable);

                    object result = astNode.Eval(environment);

                    Console.WriteLine(result);
                }
            }
        }
    }
}