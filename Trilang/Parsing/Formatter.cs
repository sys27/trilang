using System.Text;
using Trilang.Parsing.Ast;

namespace Trilang.Parsing;

public class Formatter : IVisitor
{
    private readonly Builder sb;

    public Formatter()
    {
        sb = new Builder();
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
        sb.Indent();
        sb.AppendLine("{");

        foreach (var statement in blockStatementNode.Statements)
            statement.Accept(this);

        sb.Unindent();
        sb.AppendLine("}");
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

    private sealed class Builder
    {
        private readonly int indentationSize;
        private readonly StringBuilder sb;

        private Indentation indentation;

        public Builder(int indentationSize = 4)
        {
            this.indentationSize = indentationSize;

            sb = new StringBuilder();
            indentation = new Indentation(0);
        }

        public void Indent()
            => indentation++;

        public void Unindent()
            => indentation--;

        private Builder AppendIndentation()
        {
            if (indentation == 0)
                return this;

            var size = indentation * indentationSize;
            if (size <= 256)
            {
                Span<char> span = stackalloc char[size];
                span.Fill(' ');

                sb.Append(span);
            }
            else
            {
                Span<char> span = stackalloc char[indentationSize];
                span.Fill(' ');

                for (var i = 0; i < indentation; i++)
                    sb.Append(span);
            }

            return this;
        }

        public Builder Append(string value)
        {
            sb.Append(value);

            return this;
        }

        public Builder Append(char value)
        {
            sb.Append(value);

            return this;
        }

        public Builder Append(object value)
        {
            sb.Append(value);

            return this;
        }

        public Builder AppendLine(string value)
        {
            sb.AppendLine(value);
            AppendIndentation();

            return this;
        }

        public Builder AppendLine()
        {
            sb.AppendLine();
            AppendIndentation();

            return this;
        }

        public override string ToString()
            => sb.ToString();
    }

    private readonly struct Indentation
    {
        private readonly int level;

        public Indentation(int level)
        {
            if (level < 0)
                throw new ArgumentOutOfRangeException(nameof(level));

            this.level = level;
        }

        public static Indentation operator ++(Indentation indentation)
            => new Indentation(indentation.level + 1);

        public static Indentation operator --(Indentation indentation)
            => new Indentation(indentation.level - 1);

        public static implicit operator int(Indentation indentation)
            => indentation.level;
    }
}