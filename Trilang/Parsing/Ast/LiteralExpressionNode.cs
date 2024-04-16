namespace Trilang.Parsing.Ast;

public class LiteralExpressionNode : IExpressionNode, IEquatable<LiteralExpressionNode>
{
    public LiteralExpressionNode(object value)
    {
        Value = value;
    }

    public static bool operator ==(LiteralExpressionNode? left, LiteralExpressionNode? right)
        => Equals(left, right);

    public static bool operator !=(LiteralExpressionNode? left, LiteralExpressionNode? right)
        => !Equals(left, right);

    public bool Equals(LiteralExpressionNode? other)
    {
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (obj.GetType() != GetType())
            return false;

        return Equals((LiteralExpressionNode)obj);
    }

    public override int GetHashCode()
        => HashCode.Combine(Value);

    public void Accept(IVisitor visitor)
        => visitor.Visit(this);

    public object Value { get; }
}