using System.Collections.Generic;
using Stone.AST;

namespace Stone.Parsers
{
    public class FunctionParser : BasicParser
    {
        private Parser parameter;

        private Parser parameters;

        private Parser arguments;

        public FunctionParser()
        {
            // parameter : IDENTIFIER
            this.parameter = Parser.Rule().Identifier(this.ReservedKeywords);

            // parameters : parameter { "," parameter }
            this.parameters = Parser.Rule(typeof(ParameterList)).Ast(this.parameter)
                .Repeat(Parser.Rule().Separator(new List<string> { "," }).Ast(this.parameter));

            // parameterList : "(" [ parameters ] ")"
            this.ParameterList = Parser.Rule().Separator(new List<string> { "(" })
                .Maybe(this.parameters).Separator(new List<string> { ")" });

            // def : "def" IDENTIFIER parameterList block
            this.Def = Parser.Rule(typeof(DefStatement)).Separator(new List<string> { "def" })
                .Identifier(this.ReservedKeywords).Ast(this.ParameterList).Ast(this.Block);

            // arguments : expression { "," expression }
            this.arguments = Parser.Rule(typeof(Arguments)).Ast(this.Expression)
                .Repeat(Parser.Rule().Separator(new List<string> { "," }).Ast(this.Expression));

            // postfix : "(" [ arguments ] ")"
            this.Postfix = Parser.Rule().Separator(new List<string> { "(" })
                .Maybe(this.arguments).Separator(new List<string> { ")" });

            this.ReservedKeywords.Add(")");

            // primary : ( "(" expression ")" | NUMBER | IDENTIFIER | STRING ) { postfix }
            this.Primary.Repeat(this.Postfix);

            // simple : expression [ arguments ]
            this.Simple.Option(this.arguments);

            // program : [ def | statement ] (";" | EOL)
            this.Program.InsertChoice(this.Def);
        }

        protected Parser ParameterList
        {
            get;
        }

        protected Parser Def
        {
            get;
        }

        protected Parser Postfix
        {
            get;
        }
    }
}