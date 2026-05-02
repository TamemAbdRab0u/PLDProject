using System;
using System.Collections.Generic;
using System.Text;

namespace CompilerProject
{
    public enum TokenType
    {
        Keyword, Identifier, Number, Assign, ArithmeticOperator, Operator,Parenthesis, Colon, Comma, Delimiter, EndOfFile, Unknown
    }

    public class Token
    {
        public string Value { get; set; }
        public TokenType Type { get; set; }
        public int Position { get; set; }

        public Token(string value, TokenType type, int position)
        {
            Value = value;
            Type = type;
            Position = position;
        }
    }

    public class Scanner
    {
        private enum State
        {
            Start, InId, InNum, InFloat, InOp, Done
        }

        private static readonly HashSet<string> Keywords = new HashSet<string>
        {
            "Start", "End", "if", "ef", "else", "for", "in", "range", "switch", "case", "default"
        };

        public static List<Token> Tokenize(string input)
        {
            var tokens = new List<Token>();
            int i = 0;

            while (i < input.Length)
            {
                if (char.IsWhiteSpace(input[i]))
                {
                    i++;
                    continue;
                }

                State state = State.Start;
                StringBuilder lexeme = new StringBuilder();
                int startPos = i;
                TokenType? finalType = null;

                while (state != State.Done && i < input.Length)
                {
                    char c = input[i];
                    switch (state)
                    {
                        case State.Start:
                            if (char.IsLetter(c) || c == '_') { state = State.InId; lexeme.Append(c); i++; }
                            else if (char.IsDigit(c)) { state = State.InNum; lexeme.Append(c); i++; }
                            else if (c == '=') { state = State.InOp; lexeme.Append(c); i++; }
                            else if (c == '<') { state = State.InOp; lexeme.Append(c); i++; }
                            else if (c == '>') { state = State.InOp; lexeme.Append(c); i++; }
                            else if (c == '!') { state = State.InOp; lexeme.Append(c); i++; }
                            else if (c == '*') { state = State.InOp; lexeme.Append(c); i++; }
                            else if (c == '~') { state = State.InOp; lexeme.Append(c); i++; } 
                            else if ("+-/%".Contains(c)) 
                            { 
                                lexeme.Append(c); i++; 
                                finalType = TokenType.ArithmeticOperator; state = State.Done; 
                            }
                            else if (c == '(' || c == ')') 
                            { 
                                lexeme.Append(c); i++; 
                                finalType = TokenType.Parenthesis; state = State.Done; 
                            }
                            else if (c == ':') { lexeme.Append(c); i++; finalType = TokenType.Colon; state = State.Done; }
                            else if (c == ',') { lexeme.Append(c); i++; finalType = TokenType.Comma; state = State.Done; }
                            else { lexeme.Append(c); i++; finalType = TokenType.Unknown; state = State.Done; }
                            break;

                        case State.InId:
                            if (char.IsLetterOrDigit(c) || c == '_') { lexeme.Append(c); i++; }
                            else { state = State.Done; }
                            break;

                        case State.InNum:
                            if (char.IsDigit(c)) { lexeme.Append(c); i++; }
                            else if (c == '.') { state = State.InFloat; lexeme.Append(c); i++; }
                            else { state = State.Done; }
                            break;

                        case State.InFloat:
                            if (char.IsDigit(c)) { lexeme.Append(c); i++; }
                            else { state = State.Done; }
                            break;

                        case State.InOp:
                            string currentLex = lexeme.ToString();
                            if (currentLex == "=")
                            {
                                if (c == '=') { lexeme.Append(c); i++; finalType = TokenType.Operator; state = State.Done; }
                                else { finalType = TokenType.Assign; state = State.Done; }
                            }
                            else if (currentLex == "<")
                            {
                                if (c == '=') { lexeme.Append(c); i++; finalType = TokenType.Operator; state = State.Done; }
                                else if (c == '~') { lexeme.Append(c); i++; finalType = TokenType.Delimiter; state = State.Done; }
                                else { finalType = TokenType.Operator; state = State.Done; }
                            }
                            else if (currentLex == ">")
                            {
                                if (c == '=') { lexeme.Append(c); i++; finalType = TokenType.Operator; state = State.Done; }
                                else { finalType = TokenType.Operator; state = State.Done; }
                            }
                            else if (currentLex == "~")
                            {
                                if (c == '>') { lexeme.Append(c); i++; finalType = TokenType.Delimiter; state = State.Done; }
                                else { finalType = TokenType.Unknown; state = State.Done; }
                            }
                            else if (currentLex == "!")
                            {
                                if (c == '=') { lexeme.Append(c); i++; finalType = TokenType.Operator; state = State.Done; }
                                else { finalType = TokenType.Unknown; state = State.Done; }
                            }
                            else if (currentLex == "*")
                            {
                                if (c == '*') { lexeme.Append(c); i++; finalType = TokenType.ArithmeticOperator; state = State.Done; }
                                else { finalType = TokenType.ArithmeticOperator; state = State.Done; }
                            }
                            break;
                    }
                }

                string finalValue = lexeme.ToString();

                if (finalType == null)
                {
                    if (Keywords.Contains(finalValue)) finalType = TokenType.Keyword;
                    else if (char.IsDigit(finalValue[0])) finalType = TokenType.Number;
                    else finalType = TokenType.Identifier;
                }

                tokens.Add(new Token(finalValue, finalType.Value, startPos));
            }

            tokens.Add(new Token("EOF", TokenType.EndOfFile, input.Length));
            return tokens;
        }
    }
}
