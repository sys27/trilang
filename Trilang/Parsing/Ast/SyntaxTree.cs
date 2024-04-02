namespace Trilang.Parsing.Ast;

public class SyntaxTree : IEquatable<SyntaxTree>
{
    public SyntaxTree(List<FunctionDefinitionNode> functions)
    {
        Functions = functions;
    }

    public static bool operator ==(SyntaxTree? left, SyntaxTree? right)
        => Equals(left, right);

    public static bool operator !=(SyntaxTree? left, SyntaxTree? right)
        => !Equals(left, right);

    public bool Equals(SyntaxTree? other)
    {
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Functions.SequenceEqual(other.Functions);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (obj.GetType() != GetType())
            return false;

        return Equals((SyntaxTree)obj);
    }

    public override int GetHashCode()
        => Functions.GetHashCode();

    public List<FunctionDefinitionNode> Functions { get; }
}