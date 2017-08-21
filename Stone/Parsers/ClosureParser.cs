using System.Collections.Generic;
using Stone.AST;

namespace Stone.Parsers
{
    public partial class BasicParser
    {
        private void InitializeClosureGrammar()
        {
            // primary : ( "(" expression ")" | NUMBER | IDENTIFIER | STRING ) { postfix } | "fun" parameterList block
            this.primary.InsertChoice(
                Parser.Rule(typeof(AnonymousFunction)).Separator(new List<string> { "fun" })
                .Ast(this.parameterList).Ast(this.block));
        }
    }
}