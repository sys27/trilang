namespace Trilang.Parsing.Ast;

public interface ISyntaxNode
{
    void Accept(IVisitor visitor);
}