using Trilang.Parsing;
using Trilang.Parsing.Ast;

namespace Trilang.Tests.Parsing;

public class ParserTests
{
    [Test]
    public void ParseTest()
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
}