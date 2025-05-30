using LFA.Labs.Lexer;
using System;
using System.Collections.Generic;

namespace lab3.Lexer
{
    public class Tokenizer
    {
        private readonly string _input;
        private int _position;
        private int _line;
        private int _column;

        public Tokenizer(string input)
        {
            _input = input;
            _position = 0;
            _line = 1;
            _column = 1;
        }

        public List<Token> Tokenize()
        {
            var tokens = new List<Token>();

            while (_position < _input.Length)
            {
                char current = _input[_position];

                if (char.IsWhiteSpace(current))
                {
                    if (current == '\n')
                    {
                        _line++;
                        _column = 1;
                    }
                    else
                    {
                        _column++;
                    }

                    _position++;
                    continue;
                }

                Token? token = MatchToken(current);
                if (token != null)
                {
                    tokens.Add(token);
                }
                else
                {
                    throw new Exception($"Unexpected character '{current}' at line {_line}, column {_column}");
                }

                _position++;
                _column++;
            }

            tokens.Add(new Token(TokenType.EOF, "", _line, _column));
            return tokens;
        }

        private Token? MatchToken(char ch)
        {
            return ch switch
            {
                'a' => new Token(TokenType.A, "a", _line, _column),
                'b' => new Token(TokenType.B, "b", _line, _column),
                'c' => new Token(TokenType.C, "c", _line, _column),
                'd' => new Token(TokenType.D, "d", _line, _column),
                _ => null
            };
        }
    }
}
