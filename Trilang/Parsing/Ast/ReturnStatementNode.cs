namespace Trilang.Parsing.Ast;

public class ReturnStatementNode : IExpressionNode
{
    public ReturnStatementNode(IExpressionNode value)
    {
        Value = value;
    }

    public IExpressionNode Value { get; }
}