using System.Collections.Generic;
using Stone.AST;

namespace Stone.Parsers
{
    public class ClosureParser : FunctionParser
    {
        public ClosureParser()
        {
            this.Primary.InsertChoice(
                Parser.Rule(typeof(AnonymousFunction)).Separator(new List<string> { "fun" })
                .Ast(this.ParameterList).Ast(this.Block));
        }
    }
}