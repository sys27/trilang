namespace Trilang.Parsing.Ast;

public class BlockStatementNode : IStatementNode, IEquatable<BlockStatementNode>
{
    public BlockStatementNode(List<IStatementNode> statements)
    {
        Statements = statements;
    }

    public static bool operator ==(BlockStatementNode? left, BlockStatementNode? right)
        => Equals(left, right);

    public static bool operator !=(BlockStatementNode? left, BlockStatementNode? right)
        => !Equals(left, right);

    public bool Equals(BlockStatementNode? other)
    {
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Statements.SequenceEqual(other.Statements);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (obj.GetType() != GetType())
            return false;

        return Equals((BlockStatementNode)obj);
    }

    public override int GetHashCode()
        => Statements.GetHashCode();

    public void Accept(IVisitor visitor)
        => visitor.Visit(this);

    public List<IStatementNode> Statements { get; }
}