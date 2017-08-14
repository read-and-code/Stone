using System.Collections.Generic;
using Stone.AST;
using Stone.Tokens;

namespace Stone.Parsers
{
    public class BasicParser
    {
        private Operators operators = new Operators();

        private Parser basicExpression = Parser.Rule();

        private Parser basicStatement = Parser.Rule();

        public BasicParser()
        {
            this.ReservedKeywords = new HashSet<string>();
            this.ReservedKeywords.Add(";");
            this.ReservedKeywords.Add("}");
            this.ReservedKeywords.Add(Token.EOL);

            // primary : "(" expression ")" | NUMBER | IDENTIFIER | STRING
            this.Primary = Parser.Rule(typeof(PrimaryExpression))
                .Or(new List<Parser>
                    {
                        Parser.Rule().Separator(new List<string> { "(" }).Ast(this.basicExpression)
                            .Separator(new List<string> { ")" }),
                        Parser.Rule().Number(typeof(NumberLiteral)),
                        Parser.Rule().Identifier(typeof(Name), this.ReservedKeywords),
                        Parser.Rule().String(typeof(StringLiteral)),
                    });

            // factor : "-" primary | primary
            this.Factor = Parser.Rule()
                .Or(new List<Parser>
                    {
                        Parser.Rule(typeof(NegativeExpression)).Separator(new List<string> { "-" })
                            .Ast(this.Primary),
                        this.Primary,
                    });

            // expression : factor { OPERATOR factor }
            this.Expression = this.basicExpression.Expression(typeof(BinaryExpression), this.Factor, this.operators);

            // block : "{" [ statement ] { (";" | EOL) [ statement ] } "}"
            this.Block = Parser.Rule(typeof(BlockStatement))
                .Separator(new List<string> { "{" }).Option(this.basicStatement)
                .Repeat(Parser.Rule().Separator(new List<string> { ";", Token.EOL })
                    .Option(this.basicStatement))
                .Separator(new List<string> { "}" });

            // expression
            this.Simple = Parser.Rule(typeof(PrimaryExpression)).Ast(this.Expression);

            // statement : "if" expression block [ "else" block ]
            //           | "while" expression block
            //           | simple
            this.Statement = this.basicStatement
                .Or(new List<Parser>
                    {
                        Parser.Rule(typeof(IfStatement)).Separator(new List<string> { "if" })
                            .Ast(this.Expression).Ast(this.Block)
                            .Option(Parser.Rule().Separator(new List<string> { "else" }).Ast(this.Block)),
                        Parser.Rule(typeof(WhileStatement)).Separator(new List<string> { "while" })
                            .Ast(this.Expression).Ast(this.Block),
                        this.Simple,
                    });

            // program : [ statement ] (";" | EOL)
            this.Program = Parser.Rule()
                .Or(new List<Parser>
                    {
                        this.Statement,
                        Parser.Rule(typeof(NullStatement)),
                    })
                .Separator(new List<string> { ";", Token.EOL });

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

        protected Parser Primary
        {
            get;
            private set;
        }

        protected Parser Factor
        {
            get;
            private set;
        }

        protected Parser Expression
        {
            get;
            private set;
        }

        protected Parser Block
        {
            get;
            private set;
        }

        protected Parser Simple
        {
            get;
            private set;
        }

        protected Parser Statement
        {
            get;
            private set;
        }

        protected Parser Program
        {
            get;
            private set;
        }

        protected HashSet<string> ReservedKeywords
        {
            get;
            private set;
        }

        public ASTree Parse(Lexer lexer)
        {
            return this.Program.Parse(lexer);
        }
    }
}