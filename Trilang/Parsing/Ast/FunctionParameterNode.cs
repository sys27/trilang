namespace Trilang.Parsing.Ast;

public class FunctionParameterNode : ISyntaxNode, IEquatable<FunctionParameterNode>
{
    public FunctionParameterNode(IdentifierNode name, IdentifierNode type)
    {
        Name = name;
        Type = type;
    }

    public static bool operator ==(FunctionParameterNode? left, FunctionParameterNode? right)
        => Equals(left, right);

    public static bool operator !=(FunctionParameterNode? left, FunctionParameterNode? right)
        => !Equals(left, right);

    public bool Equals(FunctionParameterNode? other)
    {
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Name.Equals(other.Name) && Type.Equals(other.Type);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (obj.GetType() != GetType())
            return false;

        return Equals((FunctionParameterNode)obj);
    }

    public override int GetHashCode()
        => HashCode.Combine(Name, Type);

    public IdentifierNode Name { get; }

    public IdentifierNode Type { get; }
}