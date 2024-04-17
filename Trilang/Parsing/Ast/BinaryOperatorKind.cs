namespace Trilang.Parsing.Ast;

public enum BinaryOperatorKind
{
    Add,
    Subtract,
    Multiply,
    Divide,
}

public static class BinaryOperatorKindExtensions
{
    public static int GetPrecedence(this BinaryOperatorKind kind)
        => kind switch
        {
            BinaryOperatorKind.Add or BinaryOperatorKind.Subtract => 1,
            BinaryOperatorKind.Multiply or BinaryOperatorKind.Divide => 2,

            _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null)
        };
}