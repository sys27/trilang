namespace Trilang.Parsing.Ast;

public class IdentifierNode : ISyntaxNode, IEquatable<IdentifierNode>
{
    public IdentifierNode(string name)
    {
        Name = name;
    }

    public static bool operator ==(IdentifierNode? left, IdentifierNode? right)
        => Equals(left, right);

    public static bool operator !=(IdentifierNode? left, IdentifierNode? right)
        => !Equals(left, right);

    public bool Equals(IdentifierNode? other)
    {
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (obj.GetType() != GetType())
            return false;

        return Equals((IdentifierNode)obj);
    }

    public override int GetHashCode()
        => Name.GetHashCode();

    public void Accept(IVisitor visitor)
        => visitor.Visit(this);

    public string Name { get; }
}