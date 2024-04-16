namespace Trilang.Parsing.Ast;

public class VariableDeclarationNode : IStatementNode, IEquatable<VariableDeclarationNode>
{
    public VariableDeclarationNode(IdentifierNode name, IdentifierNode type, IExpressionNode? value)
    {
        Name = name;
        Type = type;
        Value = value;
    }

    public static bool operator ==(VariableDeclarationNode? left, VariableDeclarationNode? right)
        => Equals(left, right);

    public static bool operator !=(VariableDeclarationNode? left, VariableDeclarationNode? right)
        => !Equals(left, right);

    public bool Equals(VariableDeclarationNode? other)
    {
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Name.Equals(other.Name) &&
               Type.Equals(other.Type) &&
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

        return Equals((VariableDeclarationNode)obj);
    }

    public override int GetHashCode()
        => HashCode.Combine(Name, Type, Value);

    public void Accept(IVisitor visitor)
        => visitor.Visit(this);

    public IdentifierNode Name { get; }

    public IdentifierNode Type { get; }

    public IExpressionNode? Value { get; }
}