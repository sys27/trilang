namespace Trilang.Lexing;

public class Token : IEquatable<Token>
{
    public Token(TokenKind kind, object? value)
    {
        Kind = kind;
        Value = value;
    }

    public static Token Number(int value)
        => new Token(TokenKind.Number, value);

    public static Token EndOfFile()
        => new Token(TokenKind.EndOfFile, null);

    public static bool operator ==(Token? left, Token? right)
        => Equals(left, right);

    public static bool operator !=(Token? left, Token? right)
        => !Equals(left, right);

    public bool Equals(Token? other)
    {
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Kind == other.Kind &&
               Equals(Value, other.Value);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (obj.GetType() != GetType())
            return false;

        return Equals((Token)obj);
    }

    public override int GetHashCode()
        => HashCode.Combine((int)Kind, Value);

    public TokenKind Kind { get; }

    public object? Value { get; }

    public int AsNumber()
        => (int)Value!;
}