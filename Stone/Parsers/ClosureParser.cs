using System.Collections.Generic;
using Stone.AST;

namespace Stone.Parsers
{
    public class ClosureParser : FunctionParser
    {
        public ClosureParser()
        {
            // primary : ( "(" expression ")" | NUMBER | IDENTIFIER | STRING ) { postfix } | "fun" parameterList block
            this.Primary.InsertChoice(
                Parser.Rule(typeof(AnonymousFunction)).Separator(new List<string> { "fun" })
                .Ast(this.ParameterList).Ast(this.Block));
        }
    }
}