using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Stone.Exceptions;
using Stone.Tokens;

namespace Stone
{
    public class Lexer
    {
        private static string regexPattern =
            @"\s*((//.*)|([0-9]+)|(""(\\""|\\\\|\\n|[^""])*"")" +
            @"|[A-Z_a-z][A-Z_a-z0-9]*|!=|==|<=|>=|&&|\+\+|--|\+|-|<|>|=|\|\||\p{P})?";

        private Regex regex = new Regex(regexPattern, RegexOptions.Compiled);

        private List<Token> queue = new List<Token>();

        private bool hasMore;

        private LineNumberReader lineNumberReader;

        public Lexer(Stream stream)
        {
            this.hasMore = true;
            this.lineNumberReader = new LineNumberReader(stream);
        }

        public Token Read()
        {
            if (this.FillQueue(0))
            {
                Token token = this.queue[0];

                this.queue.RemoveAt(0);

                return token;
            }
            else
            {
                return Token.EOF;
            }
        }

        public Token Peek(int i)
        {
            if (this.FillQueue(i))
            {
                return this.queue[i];
            }
            else
            {
                return Token.EOF;
            }
        }

        protected void ReadLine()
        {
            string line;

            try
            {
                line = this.lineNumberReader.ReadLine();
            }
            catch (IOException exception)
            {
                throw new ParseException(exception);
            }

            if (string.IsNullOrEmpty(line))
            {
                this.hasMore = false;

                return;
            }

            int lineNumber = this.lineNumberReader.LineNumber;
            Match match = this.regex.Match(line);

            while (match.Success)
            {
                this.AddToken(lineNumber, match);

                match = match.NextMatch();
            }
        }

        protected void AddToken(int lineNumber, Match match)
        {
            Group group = match.Groups[1];

            if (group.Success)
            {
                Token token;

                if (match.Groups[3].Success)
                {
                    token = new NumberToken(lineNumber, Convert.ToInt32(group.Value));
                }
                else if (match.Groups[4].Success)
                {
                    token = new StringToken(lineNumber, this.ConvertToStringLiteral(group.Value));
                }
                else
                {
                    token = new IdentifierToken(lineNumber, group.Value);
                }

                this.queue.Add(token);
            }
        }

        protected string ConvertToStringLiteral(string value)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int length = value.Length - 1;

            for (int i = 0; i < length; i++)
            {
                char current = value[i];

                if (current == '\\' && i + 1 < length)
                {
                    int next = value[i + 1];

                    if (next == '"' || next == '\\')
                    {
                        current = value[++i];
                    }
                    else if (next == 'n')
                    {
                        ++i;
                        current = '\n';
                    }
                }

                stringBuilder.Append(current);
            }

            return stringBuilder.ToString();
        }

        private bool FillQueue(int i)
        {
            while (i >= this.queue.Count)
            {
                if (this.hasMore)
                {
                    this.ReadLine();
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}