namespace Trilang.Parsing.Ast;

public class BinaryExpressionNode : IExpressionNode, IEquatable<BinaryExpressionNode>
{
    public BinaryExpressionNode(IExpressionNode left, IExpressionNode right, BinaryOperatorKind @operator)
    {
        Left = left;
        Right = right;
        Operator = @operator;
    }

    public static bool operator ==(BinaryExpressionNode? left, BinaryExpressionNode? right)
        => Equals(left, right);

    public static bool operator !=(BinaryExpressionNode? left, BinaryExpressionNode? right)
        => !Equals(left, right);

    public bool Equals(BinaryExpressionNode? other)
    {
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Left.Equals(other.Left) &&
               Right.Equals(other.Right) &&
               Operator == other.Operator;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (obj.GetType() != GetType())
            return false;

        return Equals((BinaryExpressionNode)obj);
    }

    public override int GetHashCode()
        => HashCode.Combine(Left, Right, (int)Operator);

    public void Accept(IVisitor visitor)
        => visitor.Visit(this);

    public IExpressionNode Left { get; }

    public IExpressionNode Right { get; }

    public BinaryOperatorKind Operator { get; }
}