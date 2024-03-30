namespace Trilang.Parsing.Ast;

public class FunctionDefinitionNode : ISyntaxNode, IEquatable<FunctionDefinitionNode>
{
    public FunctionDefinitionNode(
        IdentifierNode name,
        List<FunctionParameterNode> parameters,
        BlockStatementNode body,
        IdentifierNode returnType)
    {
        Name = name;
        Parameters = parameters;
        Body = body;
        ReturnType = returnType;
    }

    public static bool operator ==(FunctionDefinitionNode? left, FunctionDefinitionNode? right)
        => Equals(left, right);

    public static bool operator !=(FunctionDefinitionNode? left, FunctionDefinitionNode? right)
        => !Equals(left, right);

    public bool Equals(FunctionDefinitionNode? other)
    {
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Name.Equals(other.Name) &&
               Parameters.Equals(other.Parameters) &&
               Body.Equals(other.Body) &&
               ReturnType.Equals(other.ReturnType);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (obj.GetType() != GetType())
            return false;

        return Equals((FunctionDefinitionNode)obj);
    }

    public override int GetHashCode()
        => HashCode.Combine(Name, Parameters, Body, ReturnType);

    public IdentifierNode Name { get; }

    public List<FunctionParameterNode> Parameters { get; }

    public BlockStatementNode Body { get; }

    public IdentifierNode ReturnType { get; }
}