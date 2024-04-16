using System.Text;
using Trilang.Parsing.Ast;

namespace Trilang.Parsing;

public class Formatter : IVisitor
{
    private readonly StringBuilder sb;

    public Formatter()
    {
        sb = new StringBuilder();
    }

    public override string ToString()
        => sb.ToString();

    private static string GetBinaryOperator(BinaryOperatorKind kind)
        => kind switch
        {
            BinaryOperatorKind.Add => "+",
            BinaryOperatorKind.Subtract => "-",
            BinaryOperatorKind.Multiply => "*",
            BinaryOperatorKind.Divide => "/",

            _ => throw new ArgumentOutOfRangeException(),
        };

    public void Visit(BinaryExpressionNode binaryExpressionNode)
    {
        var left = binaryExpressionNode.Left;
        if (left is BinaryExpressionNode)
        {
            sb.Append('(');
            left.Accept(this);
            sb.Append(')');
        }
        else
        {
            left.Accept(this);
        }

        sb.Append(' ');
        sb.Append(GetBinaryOperator(binaryExpressionNode.Operator));
        sb.Append(' ');

        var right = binaryExpressionNode.Right;
        if (right is BinaryExpressionNode)
        {
            sb.Append('(');
            right.Accept(this);
            sb.Append(')');
        }
        else
        {
            right.Accept(this);
        }
    }

    public void Visit(BlockStatementNode blockStatementNode)
    {
        sb.Append("{\n");

        foreach (var statement in blockStatementNode.Statements)
            statement.Accept(this);

        sb.Append("}\n");
    }

    public void Visit(FunctionDefinitionNode functionDefinition)
    {
        sb.Append("function ");
        functionDefinition.Name.Accept(this);

        sb.Append('(');
        for (var index = 0; index < functionDefinition.Parameters.Count; index++)
        {
            functionDefinition.Parameters[index].Accept(this);

            if (index != functionDefinition.Parameters.Count - 1)
                sb.Append(", ");
        }

        sb.Append("): ");
        functionDefinition.ReturnType.Accept(this);
        sb.Append(' ');

        functionDefinition.Body.Accept(this);
    }

    public void Visit(FunctionParameterNode functionParameterNode)
    {
        functionParameterNode.Name.Accept(this);
        sb.Append(": ");
        functionParameterNode.Type.Accept(this);
    }

    public void Visit(IdentifierExpressionNode identifierExpressionNode)
    {
        sb.Append(identifierExpressionNode.Value);
    }

    public void Visit(IdentifierNode identifierNode)
    {
        sb.Append(identifierNode.Name);
    }

    public void Visit(LiteralExpressionNode literalExpressionNode)
    {
        sb.Append(literalExpressionNode.Value);
    }

    public void Visit(ReturnStatementNode returnStatementNode)
    {
        sb.Append("return ");
        returnStatementNode.Value.Accept(this);
        sb.Append(";\n");
    }

    public void Visit(SyntaxTree syntaxTree)
    {
        foreach (var function in syntaxTree.Functions)
            function.Accept(this);
    }

    public void Visit(VariableDeclarationNode variableDeclarationNode)
    {
        throw new NotImplementedException();
    }
}