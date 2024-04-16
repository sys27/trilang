namespace Trilang.Parsing.Ast;

public class ReturnStatementNode : IStatementNode, IEquatable<ReturnStatementNode>
{
    public ReturnStatementNode(IExpressionNode value)
    {
        Value = value;
    }

    public static bool operator ==(ReturnStatementNode? left, ReturnStatementNode? right)
        => Equals(left, right);

    public static bool operator !=(ReturnStatementNode? left, ReturnStatementNode? right)
        => !Equals(left, right);

    public bool Equals(ReturnStatementNode? other)
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

        return Equals((ReturnStatementNode)obj);
    }

    public override int GetHashCode()
        => Value.GetHashCode();

    public void Accept(IVisitor visitor)
        => visitor.Visit(this);

    public IExpressionNode Value { get; }
}