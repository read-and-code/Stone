using System.Collections.Generic;
using Stone.AST;
using Stone.Tokens;

namespace Stone.Parsers
{
    public partial class BasicParser
    {
        private Parser member;

        private Parser classBody;

        private Parser defClass;

        private void InitializeClassGrammar()
        {
            // member : def | simple
            this.member = Parser.Rule().Or(new List<Parser> { this.def, this.simple });

            // classBody : "{" [ member ] { (";" | EOL) [ member ] } "}"
            this.classBody = Parser.Rule(typeof(ClassBody)).Separator(new List<string> { "{" })
                .Option(this.member)
                .Repeat(Parser.Rule().Separator(new List<string> { ";", Token.EOL }).Option(this.member))
                .Separator(new List<string> { "}" });

            // defClass : "class" IDENTIFIER [ "extends" IDENTIFIER ] classBody
            this.defClass = Parser.Rule(typeof(ClassStatement)).Separator(new List<string> { "class" })
                .Identifier(this.reservedKeywords)
                .Option(Parser.Rule().Separator(new List<string> { "extends" }).Identifier(this.reservedKeywords))
                .Ast(this.classBody);

            // "." IDENTIFIER | "(" [ arguments ] ")"
            this.postfix.InsertChoice(Parser.Rule(typeof(Dot)).Separator(new List<string> { "." }).Identifier(this.reservedKeywords));

            // [ defClass | def | statement ] (";" | EOL)
            this.program.InsertChoice(this.defClass);
        }
    }
}