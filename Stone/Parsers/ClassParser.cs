using System.Collections.Generic;
using Stone.AST;
using Stone.Tokens;

namespace Stone.Parsers
{
    public class ClassParser : ClosureParser
    {
        private Parser member;

        private Parser classBody;

        private Parser defClass;

        public ClassParser()
        {
            // member : def | simple
            this.member = Parser.Rule().Or(new List<Parser> { this.Def, this.Simple });

            // classBody : "{" [ member ] { (";" | EOL) [ member ] } "}"
            this.classBody = Parser.Rule(typeof(ClassBody)).Separator(new List<string> { "{" })
                .Option(this.member)
                .Repeat(Parser.Rule().Separator(new List<string> { ";", Token.EOL }).Option(this.member))
                .Separator(new List<string> { "}" });

            // defClass : "class" IDENTIFIER [ "extends" IDENTIFIER ] classBody
            this.defClass = Parser.Rule(typeof(ClassStatement)).Separator(new List<string> { "class" })
                .Identifier(this.ReservedKeywords)
                .Option(Parser.Rule().Separator(new List<string> { "extends" }).Identifier(this.ReservedKeywords))
                .Ast(this.classBody);

            // "." IDENTIFIER | "(" [ arguments ] ")"
            this.Postfix.InsertChoice(Parser.Rule(typeof(Dot)).Separator(new List<string> { "." }).Identifier(this.ReservedKeywords));

            // [ defClass | def | statement ] (";" | EOL)
            this.Program.InsertChoice(this.defClass);
        }
    }
}