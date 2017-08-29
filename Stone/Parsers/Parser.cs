using System;
using System.Collections.Generic;
using Stone.AST;

namespace Stone.Parsers
{
    public class Parser
    {
        public Parser()
        {
            this.Elements = new List<Element>();
        }

        public Parser(Type type)
            : this()
        {
            this.Type = type;
        }

        protected Parser(Parser parser)
            : this()
        {
            this.Elements = parser.Elements;
            this.Type = parser.Type;
        }

        private Type Type
        {
            get;
            set;
        }

        private List<Element> Elements
        {
            get;
            set;
        }

        public static Parser Rule()
        {
            return Rule(null);
        }

        public static Parser Rule(Type type)
        {
            return new Parser(type);
        }

        public ASTNode Parse(Lexer lexer)
        {
            List<ASTNode> astNodes = new List<ASTNode>();

            foreach (Element element in this.Elements)
            {
                element.Parse(lexer, astNodes);
            }

            if (this.Type == null)
            {
                if (astNodes.Count == 1)
                {
                    return astNodes[0];
                }
                else
                {
                    return new ASTBranchNode(astNodes);
                }
            }
            else if (this.Type == typeof(PrimaryExpression))
            {
                if (astNodes.Count == 1)
                {
                    return astNodes[0];
                }
                else
                {
                    return new PrimaryExpression(astNodes);
                }
            }
            else
            {
                return ASTNodeFactory.Make(this.Type, new[] { astNodes });
            }
        }

        public bool Match(Lexer lexer)
        {
            if (this.Elements.Count == 0)
            {
                return true;
            }
            else
            {
                return this.Elements[0].Match(lexer);
            }
        }

        public Parser Number(Type type)
        {
            this.Elements.Add(new NumberTokenElement(type));

            return this;
        }

        public Parser Identifier(HashSet<string> reservedKeywords)
        {
            return this.Identifier(null, reservedKeywords);
        }

        public Parser Identifier(Type type, HashSet<string> reservedKeywords)
        {
            this.Elements.Add(new IdTokenElement(type, reservedKeywords));

            return this;
        }

        public Parser String(Type type)
        {
            this.Elements.Add(new StringTokenElement(type));

            return this;
        }

        public Parser Separator(List<string> patterns)
        {
            this.Elements.Add(new Skip(patterns));

            return this;
        }

        public Parser Ast(Parser parser)
        {
            this.Elements.Add(new Tree(parser));

            return this;
        }

        public Parser Or(List<Parser> parsers)
        {
            this.Elements.Add(new OrTree(parsers));

            return this;
        }

        public Parser Maybe(Parser parser)
        {
            this.Elements.Add(new OrTree(new List<Parser> { parser, new Parser(parser.Type) }));

            return this;
        }

        public Parser Option(Parser parser)
        {
            this.Elements.Add(new Repeat(parser, true));

            return this;
        }

        public Parser Repeat(Parser parser)
        {
            this.Elements.Add(new Repeat(parser, false));

            return this;
        }

        public Parser Expression(Type type, Parser subExpression, Operators operators)
        {
            this.Elements.Add(new Expression(type, subExpression, operators));

            return this;
        }

        public Parser InsertChoice(Parser parser)
        {
            Element element = this.Elements[0];

            if (element is OrTree)
            {
                ((OrTree)element).Insert(parser);
            }
            else
            {
                Parser otherwise = new Parser(this);

                this.Elements = new List<Element>();
                this.Or(new List<Parser> { parser, otherwise });
            }

            return this;
        }
    }
}