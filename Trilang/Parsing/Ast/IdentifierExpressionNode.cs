namespace Trilang.Parsing.Ast;

public class IdentifierExpressionNode : IExpressionNode, IEquatable<IdentifierExpressionNode>
{
    public IdentifierExpressionNode(IdentifierNode identifier)
    {
        Identifier = identifier;
    }

    public static bool operator ==(IdentifierExpressionNode? left, IdentifierExpressionNode? right)
        => Equals(left, right);

    public static bool operator !=(IdentifierExpressionNode? left, IdentifierExpressionNode? right)
        => !Equals(left, right);

    public bool Equals(IdentifierExpressionNode? other)
    {
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Identifier == other.Identifier;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (obj.GetType() != GetType())
            return false;

        return Equals((IdentifierExpressionNode)obj);
    }

    public override int GetHashCode()
        => HashCode.Combine(Identifier);

    public void Accept(IVisitor visitor)
        => visitor.Visit(this);

    public IdentifierNode Identifier { get; }
}