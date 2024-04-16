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
                                new IdentifierExpressionNode("a"),
                                new IdentifierExpressionNode("b"),
                                BinaryOperatorKind.Add
                            ),
                            new IdentifierExpressionNode("c"),
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
                                new IdentifierExpressionNode("a"),
                                new IdentifierExpressionNode("b"),
                                BinaryOperatorKind.Subtract
                            ),
                            new IdentifierExpressionNode("c"),
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
                                new IdentifierExpressionNode("a"),
                                new IdentifierExpressionNode("b"),
                                BinaryOperatorKind.Multiply
                            ),
                            new IdentifierExpressionNode("c"),
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
                                new IdentifierExpressionNode("a"),
                                new IdentifierExpressionNode("b"),
                                BinaryOperatorKind.Divide
                            ),
                            new IdentifierExpressionNode("c"),
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
                                new IdentifierExpressionNode("a"),
                                new IdentifierExpressionNode("b"),
                                BinaryOperatorKind.Subtract
                            ),
                            new BinaryExpressionNode(
                                new BinaryExpressionNode(
                                    new IdentifierExpressionNode("c"),
                                    new IdentifierExpressionNode("d"),
                                    BinaryOperatorKind.Divide
                                ),
                                new IdentifierExpressionNode("e"),
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
}