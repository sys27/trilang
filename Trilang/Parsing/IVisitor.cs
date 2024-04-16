using Trilang.Parsing.Ast;

namespace Trilang.Parsing;

public interface IVisitor
{
    void Visit(BinaryExpressionNode binaryExpressionNode);
    void Visit(BlockStatementNode blockStatementNode);
    void Visit(FunctionDefinitionNode functionDefinition);
    void Visit(FunctionParameterNode functionParameterNode);
    void Visit(IdentifierExpressionNode identifierExpressionNode);
    void Visit(IdentifierNode identifierNode);
    void Visit(LiteralExpressionNode literalExpressionNode);
    void Visit(ReturnStatementNode returnStatementNode);
    void Visit(SyntaxTree syntaxTree);
    void Visit(VariableDeclarationNode variableDeclarationNode);
}