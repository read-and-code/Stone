using System.Collections.Generic;
using Stone.AST;

namespace Stone.Parsers
{
    public partial class BasicParser
    {
        private Parser elements;

        private void InitializeArrayGrammar()
        {
            // elements : expression { "," expression }
            this.elements = Parser.Rule(typeof(ArrayLiteral)).Ast(this.expression)
                .Repeat(Parser.Rule().Separator(new List<string> { "," }).Ast(this.expression));

            this.reservedKeywords.Add("]");

            // primary : ( "[" [ elements ] "]" | "(" expression ")" | NUMBER | IDENTIFIER | STRING ) { postfix } | "fun" parameterList block
            this.primary.InsertChoice(
                Parser.Rule().Separator(new List<string> { "[" }).Maybe(this.elements)
                .Separator(new List<string> { "]" }));

            // "." IDENTIFIER | "(" [ arguments ] ")" | "[" expression "]"
            this.postfix.InsertChoice(
                Parser.Rule(typeof(ArrayReference)).Separator(new List<string> { "[" })
                .Ast(this.expression).Separator(new List<string> { "]" }));
        }
    }
}