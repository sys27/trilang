namespace Trilang.Lexing;

public class Lexer
{
    public List<Token> Tokenize(string code)
    {
        var tokens = new List<Token>();

        for (var position = 0; position < code.Length;)
        {
            var c = code[position];
            var next = position + 1 < code.Length ? code[position + 1] : '\0';

            if (char.IsWhiteSpace(c) || c is '\t' or '\n' or '\r')
            {
                position++;
                continue;
            }

            if (char.IsDigit(c))
            {
                var start = position;
                while (position < code.Length && char.IsDigit(code[position]))
                {
                    position++;
                }

                tokens.Add(new Token(TokenKind.Number, int.Parse(code.AsSpan()[start..position])));
                continue;
            }

            if (char.IsLetter(c))
            {
                var start = position;
                while (position < code.Length && char.IsLetterOrDigit(code[position]))
                {
                    position++;
                }

                var id = code.AsSpan()[start..position];
                tokens.Add(id switch
                {
                    "function" => new Token(TokenKind.Function, null),
                    "var" => new Token(TokenKind.Var, null),
                    "if" => new Token(TokenKind.If, null),
                    "else" => new Token(TokenKind.Else, null),
                    "for" => new Token(TokenKind.For, null),
                    "return" => new Token(TokenKind.Return, null),

                    _ => Token.Id(id.ToString()),
                });

                continue;
            }

            var (token, size) = (c, next) switch
            {
                ('(', _) => (new Token(TokenKind.OpenParenthesis, null), 1),
                (')', _) => (new Token(TokenKind.CloseParenthesis, null), 1),
                ('{', _) => (new Token(TokenKind.OpenBrace, null), 1),
                ('}', _) => (new Token(TokenKind.CloseBrace, null), 1),
                ('[', _) => (new Token(TokenKind.OpenBracket, null), 1),
                (']', _) => (new Token(TokenKind.CloseBracket, null), 1),

                (':', _) => (new Token(TokenKind.Colon, null), 1),
                (';', _) => (new Token(TokenKind.Semicolon, null), 1),
                (',', _) => (new Token(TokenKind.Comma, null), 1),
                ('.', _) => (new Token(TokenKind.Dot, null), 1),

                ('+', _) => (new Token(TokenKind.Plus, null), 1),
                ('-', _) => (new Token(TokenKind.Minus, null), 1),
                ('*', _) => (new Token(TokenKind.Asterisk, null), 1),
                ('/', _) => (new Token(TokenKind.Slash, null), 1),

                ('=', '=') => (new Token(TokenKind.EqualsEquals, null), 2),
                ('=', _) => (new Token(TokenKind.Equals, null), 1),
                ('!', '=') => (new Token(TokenKind.NotEquals, null), 2),
                ('>', '=') => (new Token(TokenKind.GreaterThanOrEqual, null), 2),
                ('<', '=') => (new Token(TokenKind.LessThanOrEqual, null), 2),
                ('<', _) => (new Token(TokenKind.LessThan, null), 1),
                ('>', _) => (new Token(TokenKind.GreaterThan, null), 1),

                ('&', '&') => (new Token(TokenKind.AmpersandAmpersand, null), 2),
                ('|', '|') => (new Token(TokenKind.PipePipe, null), 2),
                ('&', _) => (new Token(TokenKind.Ampersand, null), 1),
                ('|', _) => (new Token(TokenKind.Pipe, null), 1),

                _ => throw new Exception($"Unexpected character '{c}' at position {position}")
            };
            tokens.Add(token);
            position += size;
        }

        tokens.Add(new Token(TokenKind.EndOfFile, null));

        return tokens;
    }
}