using System.Collections.Generic;
using Stone.AST;

namespace Stone.Parsers
{
    public class ArrayParser : FunctionParser
    {
        private Parser elements;

        public ArrayParser()
        {
            // elements : expression { "," expression }
            this.elements = Parser.Rule(typeof(ArrayLiteral)).Ast(this.Expression)
                .Repeat(Parser.Rule().Separator(new List<string> { "," }).Ast(this.Expression));

            this.ReservedKeywords.Add("]");

            // primary : ( "[" [ elements ] "]" | "(" expression ")" | NUMBER | IDENTIFIER | STRING ) { postfix } | "fun" parameterList block
            this.Primary.InsertChoice(
                Parser.Rule().Separator(new List<string> { "[" }).Maybe(this.elements)
                .Separator(new List<string> { "]" }));

            // "." IDENTIFIER | "(" [ arguments ] ")" | "[" expression "]"
            this.Postfix.InsertChoice(
                Parser.Rule(typeof(ArrayReference)).Separator(new List<string> { "[" })
                .Ast(this.Expression).Separator(new List<string> { "]" }));
        }
    }
}