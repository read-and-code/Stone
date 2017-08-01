using System.Collections.Generic;
using Stone.AST;
using Stone.Tokens;

namespace Stone.Parsers
{
    public class BasicParser
    {
        private HashSet<string> reservedKeywords = new HashSet<string>();

        private Operators operators = new Operators();

        private Parser basicExpression = Parser.Rule();

        private Parser primary;

        private Parser factor;

        private Parser expression;

        private Parser basicStatement = Parser.Rule();

        private Parser block;

        private Parser simple;

        private Parser statement;

        private Parser program;

        public BasicParser()
        {
            // primary : "(" expression ")" | NUMBER | IDENTIFIER | STRING
            this.primary = Parser.Rule(typeof(PrimaryExpression))
                .Or(new List<Parser>
                    {
                        Parser.Rule().Separator(new List<string> { "(" }).Ast(this.basicExpression)
                            .Separator(new List<string> { ")" }),
                        Parser.Rule().Number(typeof(NumberLiteral)),
                        Parser.Rule().Identifier(typeof(Name), this.reservedKeywords),
                        Parser.Rule().String(typeof(StringLiteral)),
                    });

            // factor : "-" primary | primary
            this.factor = Parser.Rule()
                .Or(new List<Parser>
                    {
                        Parser.Rule(typeof(NegativeExpression)).Separator(new List<string> { "-" })
                            .Ast(this.primary),
                        this.primary,
                    });

            // expression : factor { OP factor }
            this.expression = this.basicExpression.Expression(typeof(BinaryExpression), this.factor, this.operators);

            // block : "{" [ statement ] {(";" | EOL) [ statement ]} "}"
            this.block = Parser.Rule(typeof(BlockStatement))
                .Separator(new List<string> { "{" }).Option(this.basicStatement)
                .Repeat(Parser.Rule().Separator(new List<string> { ";", Token.EOL })
                    .Option(this.basicStatement))
                .Separator(new List<string> { "}" });

            // expression
            this.simple = Parser.Rule(typeof(PrimaryExpression)).Ast(this.expression);

            // statement : "if" expression block [ "else" block ]
            //           | "while" expression block
            //           | simple
            this.statement = this.basicStatement
                .Or(new List<Parser>
                    {
                        Parser.Rule(typeof(IfStatement)).Separator(new List<string> { "if" })
                            .Ast(this.expression).Ast(this.block)
                            .Option(Parser.Rule().Separator(new List<string> { "else" })
                                .Ast(this.block)),
                        Parser.Rule(typeof(WhileStatement)).Separator(new List<string> { "while" })
                            .Ast(this.expression).Ast(this.block),
                        this.simple,
                    });

            // program : [ statement ] (";" | EOL)
            this.program = Parser.Rule()
                .Or(new List<Parser>
                    {
                        this.statement,
                        Parser.Rule(typeof(NullStatement)).Separator(new List<string> { ";", Token.EOL }),
                    });

            this.reservedKeywords.Add(";");
            this.reservedKeywords.Add("}");
            this.reservedKeywords.Add(Token.EOL);

            this.operators.Put("=", 1, Operators.RIGHT);
            this.operators.Put("==", 2, Operators.Left);
            this.operators.Put(">", 2, Operators.Left);
            this.operators.Put("<", 2, Operators.Left);
            this.operators.Put("+", 3, Operators.Left);
            this.operators.Put("-", 3, Operators.Left);
            this.operators.Put("*", 4, Operators.Left);
            this.operators.Put("/", 4, Operators.Left);
            this.operators.Put("%", 4, Operators.Left);
        }

        public ASTree Parse(Lexer lexer)
        {
            return this.expression.Parse(lexer);
        }
    }
}