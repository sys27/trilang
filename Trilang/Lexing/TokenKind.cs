namespace Trilang.Lexing;

public enum TokenKind
{
    Whitespace,
    NewLine,
    Identifier,
    Number,
    EndOfFile,

    OpenParenthesis,
    CloseParenthesis,
    OpenBrace,
    CloseBrace,
    OpenBracket,
    CloseBracket,

    Colon,
    Semicolon,
    Comma,
    Dot,

    Plus,
    Minus,
    Asterisk,
    Slash,

    Equals,
    EqualsEquals,
    NotEquals,
    LessThan,
    LessThanOrEqual,
    GreaterThan,
    GreaterThanOrEqual,

    Ampersand,
    Pipe,
    AmpersandAmpersand,
    PipePipe,

    Function,
    Var,
    If,
    Else,
    For,
    Return,
}