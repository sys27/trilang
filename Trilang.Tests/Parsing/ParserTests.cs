using Trilang.Parsing;
using Trilang.Parsing.Ast;

namespace Trilang.Tests.Parsing;

public class ParserTests
{
    [Test]
    public void FunctionWithoutParametersTest()
    {
        const string code =
            """
            function main(): int {
                return 0;
            }
            """;
        var parse = new Parser(code);
        var tree = parse.Parse();
        var expected = new SyntaxTree([
            new FunctionDefinitionNode(
                new IdentifierNode("main"),
                [],
                new IdentifierNode("int"),
                new BlockStatementNode([
                    new ReturnStatementNode(new LiteralExpressionNode(0))
                ])
            )
        ]);

        Assert.That(tree, Is.EqualTo(expected));
    }

    [Test]
    public void FunctionWithParametersTest()
    {
        const string code =
            """
            function main(a: int, b: int): int {
                return 0;
            }
            """;
        var parse = new Parser(code);
        var tree = parse.Parse();
        var expected = new SyntaxTree([
            new FunctionDefinitionNode(
                new IdentifierNode("main"),
                [
                    new FunctionParameterNode(new IdentifierNode("a"), new IdentifierNode("int")),
                    new FunctionParameterNode(new IdentifierNode("b"), new IdentifierNode("int"))
                ],
                new IdentifierNode("int"),
                new BlockStatementNode([
                    new ReturnStatementNode(new LiteralExpressionNode(0))
                ])
            )
        ]);

        Assert.That(tree, Is.EqualTo(expected));
    }

    [Test]
    public void AddExpressionTest()
    {
        const string code =
            """
            function main(a: int, b: int): int {
                return a + b + c;
            }
            """;
        var parse = new Parser(code);
        var tree = parse.Parse();
        var expected = new SyntaxTree([
            new FunctionDefinitionNode(
                new IdentifierNode("main"),
                [
                    new FunctionParameterNode(new IdentifierNode("a"), new IdentifierNode("int")),
                    new FunctionParameterNode(new IdentifierNode("b"), new IdentifierNode("int"))
                ],
                new IdentifierNode("int"),
                new BlockStatementNode([
                    new ReturnStatementNode(
                        new BinaryExpressionNode(
                            new BinaryExpressionNode(
                                new IdentifierExpressionNode(new IdentifierNode("a")),
                                new IdentifierExpressionNode(new IdentifierNode("b")),
                                BinaryOperatorKind.Add
                            ),
                            new IdentifierExpressionNode(new IdentifierNode("c")),
                            BinaryOperatorKind.Add
                        )
                    )
                ])
            )
        ]);

        Assert.That(tree, Is.EqualTo(expected));
    }

    [Test]
    public void SubExpressionTest()
    {
        const string code =
            """
            function main(a: int, b: int): int {
                return a - b - c;
            }
            """;
        var parse = new Parser(code);
        var tree = parse.Parse();
        var expected = new SyntaxTree([
            new FunctionDefinitionNode(
                new IdentifierNode("main"),
                [
                    new FunctionParameterNode(new IdentifierNode("a"), new IdentifierNode("int")),
                    new FunctionParameterNode(new IdentifierNode("b"), new IdentifierNode("int"))
                ],
                new IdentifierNode("int"),
                new BlockStatementNode([
                    new ReturnStatementNode(
                        new BinaryExpressionNode(
                            new BinaryExpressionNode(
                                new IdentifierExpressionNode(new IdentifierNode("a")),
                                new IdentifierExpressionNode(new IdentifierNode("b")),
                                BinaryOperatorKind.Subtract
                            ),
                            new IdentifierExpressionNode(new IdentifierNode("c")),
                            BinaryOperatorKind.Subtract
                        )
                    )
                ])
            )
        ]);

        Assert.That(tree, Is.EqualTo(expected));
    }

    [Test]
    public void MulExpressionTest()
    {
        const string code =
            """
            function main(a: int, b: int): int {
                return a * b * c;
            }
            """;
        var parse = new Parser(code);
        var tree = parse.Parse();
        var expected = new SyntaxTree([
            new FunctionDefinitionNode(
                new IdentifierNode("main"),
                [
                    new FunctionParameterNode(new IdentifierNode("a"), new IdentifierNode("int")),
                    new FunctionParameterNode(new IdentifierNode("b"), new IdentifierNode("int"))
                ],
                new IdentifierNode("int"),
                new BlockStatementNode([
                    new ReturnStatementNode(
                        new BinaryExpressionNode(
                            new BinaryExpressionNode(
                                new IdentifierExpressionNode(new IdentifierNode("a")),
                                new IdentifierExpressionNode(new IdentifierNode("b")),
                                BinaryOperatorKind.Multiply
                            ),
                            new IdentifierExpressionNode(new IdentifierNode("c")),
                            BinaryOperatorKind.Multiply
                        )
                    )
                ])
            )
        ]);

        Assert.That(tree, Is.EqualTo(expected));
    }

    [Test]
    public void DivExpressionTest()
    {
        const string code =
            """
            function main(a: int, b: int): int {
                return a / b / c;
            }
            """;
        var parse = new Parser(code);
        var tree = parse.Parse();
        var expected = new SyntaxTree([
            new FunctionDefinitionNode(
                new IdentifierNode("main"),
                [
                    new FunctionParameterNode(new IdentifierNode("a"), new IdentifierNode("int")),
                    new FunctionParameterNode(new IdentifierNode("b"), new IdentifierNode("int"))
                ],
                new IdentifierNode("int"),
                new BlockStatementNode([
                    new ReturnStatementNode(
                        new BinaryExpressionNode(
                            new BinaryExpressionNode(
                                new IdentifierExpressionNode(new IdentifierNode("a")),
                                new IdentifierExpressionNode(new IdentifierNode("b")),
                                BinaryOperatorKind.Divide
                            ),
                            new IdentifierExpressionNode(new IdentifierNode("c")),
                            BinaryOperatorKind.Divide
                        )
                    )
                ])
            )
        ]);

        Assert.That(tree, Is.EqualTo(expected));
    }

    [Test]
    public void PriorityTestExpressionTest()
    {
        const string code =
            """
            function main(): int {
                return a - b + c / d * e;
            }
            """;
        var parse = new Parser(code);
        var tree = parse.Parse();
        var expected = new SyntaxTree([
            new FunctionDefinitionNode(
                new IdentifierNode("main"),
                [],
                new IdentifierNode("int"),
                new BlockStatementNode([
                    new ReturnStatementNode(
                        new BinaryExpressionNode(
                            new BinaryExpressionNode(
                                new IdentifierExpressionNode(new IdentifierNode("a")),
                                new IdentifierExpressionNode(new IdentifierNode("b")),
                                BinaryOperatorKind.Subtract
                            ),
                            new BinaryExpressionNode(
                                new BinaryExpressionNode(
                                    new IdentifierExpressionNode(new IdentifierNode("c")),
                                    new IdentifierExpressionNode(new IdentifierNode("d")),
                                    BinaryOperatorKind.Divide
                                ),
                                new IdentifierExpressionNode(new IdentifierNode("e")),
                                BinaryOperatorKind.Multiply
                            ),
                            BinaryOperatorKind.Add
                        )
                    )
                ])
            )
        ]);

        Assert.That(tree, Is.EqualTo(expected));
    }

    [Test]
    public void SamePriorityTestExpressionTest()
    {
        const string code =
            """
            function main(): int {
                return a + b - c - d + e;
            }
            """;
        var parse = new Parser(code);
        var tree = parse.Parse();
        var expected = new SyntaxTree([
            new FunctionDefinitionNode(
                new IdentifierNode("main"),
                [],
                new IdentifierNode("int"),
                new BlockStatementNode([
                    new ReturnStatementNode(
                        new BinaryExpressionNode(
                            new BinaryExpressionNode(
                                new BinaryExpressionNode(
                                    new BinaryExpressionNode(
                                        new IdentifierExpressionNode(new IdentifierNode("a")),
                                        new IdentifierExpressionNode(new IdentifierNode("b")),
                                        BinaryOperatorKind.Add
                                    ),
                                    new IdentifierExpressionNode(new IdentifierNode("c")),
                                    BinaryOperatorKind.Subtract
                                ),
                                new IdentifierExpressionNode(new IdentifierNode("d")),
                                BinaryOperatorKind.Subtract
                            ),
                            new IdentifierExpressionNode(new IdentifierNode("e")),
                            BinaryOperatorKind.Add
                        )
                    )
                ])
            )
        ]);

        Assert.That(tree, Is.EqualTo(expected));
    }

    [Test]
    public void VariableDeclarationTest()
    {
        const string code =
            """
            function main(): int {
                var a: int = 0;
            }
            """;
        var parse = new Parser(code);
        var tree = parse.Parse();
        var expected = new SyntaxTree([
            new FunctionDefinitionNode(
                new IdentifierNode("main"),
                [],
                new IdentifierNode("int"),
                new BlockStatementNode([
                    new VariableDeclarationNode(
                        new IdentifierNode("a"),
                        new IdentifierNode("int"),
                        new LiteralExpressionNode(0)
                    )
                ])
            )
        ]);

        Assert.That(tree, Is.EqualTo(expected));
    }
}