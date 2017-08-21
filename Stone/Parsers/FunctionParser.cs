using System.Collections.Generic;
using Stone.AST;

namespace Stone.Parsers
{
    public partial class BasicParser
    {
        private Parser parameter;

        private Parser parameters;

        private Parser arguments;

        private Parser parameterList;

        private Parser def;

        private Parser postfix;

        private void InitializeFunctionGrammar()
        {
            // parameter : IDENTIFIER
            this.parameter = Parser.Rule().Identifier(this.reservedKeywords);

            // parameters : parameter { "," parameter }
            this.parameters = Parser.Rule(typeof(ParameterList)).Ast(this.parameter)
                .Repeat(Parser.Rule().Separator(new List<string> { "," }).Ast(this.parameter));

            // parameterList : "(" [ parameters ] ")"
            this.parameterList = Parser.Rule().Separator(new List<string> { "(" })
                .Maybe(this.parameters).Separator(new List<string> { ")" });

            // def : "def" IDENTIFIER parameterList block
            this.def = Parser.Rule(typeof(DefStatement)).Separator(new List<string> { "def" })
                .Identifier(this.reservedKeywords).Ast(this.parameterList).Ast(this.block);

            // arguments : expression { "," expression }
            this.arguments = Parser.Rule(typeof(Arguments)).Ast(this.expression)
                .Repeat(Parser.Rule().Separator(new List<string> { "," }).Ast(this.expression));

            // postfix : "(" [ arguments ] ")"
            this.postfix = Parser.Rule().Separator(new List<string> { "(" })
                .Maybe(this.arguments).Separator(new List<string> { ")" });

            this.reservedKeywords.Add(")");

            // primary : ( "(" expression ")" | NUMBER | IDENTIFIER | STRING ) { postfix }
            this.primary.Repeat(this.postfix);

            // simple : expression [ arguments ]
            this.simple.Option(this.arguments);

            // program : [ def | statement ] (";" | EOL)
            this.program.InsertChoice(this.def);
        }
    }
}