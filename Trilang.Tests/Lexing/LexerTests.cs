using Trilang.Lexing;

namespace Trilang.Tests.Lexing;

public class LexerTests
{
    [Test]
    public void GetTokenForEmptyStringTest()
    {
        var lexer = new Lexer();
        var tokens = lexer.Tokenize(string.Empty);

        Assert.That(tokens, Has.Count.EqualTo(1));
        Assert.That(tokens[0], Is.EqualTo(Token.EndOfFile()));
    }

    [Test]
    public void SkipWhitespaceTokensTest()
    {
        var lexer = new Lexer();
        var tokens = lexer.Tokenize(" \t\n\r");

        Assert.That(tokens, Has.Count.EqualTo(1));
        Assert.That(tokens[0], Is.EqualTo(Token.EndOfFile()));
    }

    [Test]
    public void GetTokenForNumberTest()
    {
        var lexer = new Lexer();
        var tokens = lexer.Tokenize("123");
        var expected = new[]
        {
            Token.Number(123),
            Token.EndOfFile()
        };

        Assert.That(tokens, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("function", TokenKind.Function)]
    [TestCase("var", TokenKind.Var)]
    [TestCase("if", TokenKind.If)]
    [TestCase("else", TokenKind.Else)]
    [TestCase("for", TokenKind.For)]
    [TestCase("return", TokenKind.Return)]
    public void GetTokenForKeywordTest(string code, TokenKind expectedTokenKind)
    {
        var lexer = new Lexer();
        var tokens = lexer.Tokenize(code);
        var expected = new[]
        {
            new Token(expectedTokenKind, null),
            Token.EndOfFile()
        };

        Assert.That(tokens, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("abc")]
    [TestCase("abc123")]
    public void GetTokensForIdentifierTest(string id)
    {
        var lexer = new Lexer();
        var tokens = lexer.Tokenize(id);
        var expected = new[]
        {
            new Token(TokenKind.Identifier, id),
            Token.EndOfFile()
        };

        Assert.That(tokens, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("(", TokenKind.OpenParenthesis)]
    [TestCase(")", TokenKind.CloseParenthesis)]
    [TestCase("{", TokenKind.OpenBrace)]
    [TestCase("}", TokenKind.CloseBrace)]
    [TestCase("[", TokenKind.OpenBracket)]
    [TestCase("]", TokenKind.CloseBracket)]
    [TestCase(":", TokenKind.Colon)]
    [TestCase(";", TokenKind.Semicolon)]
    [TestCase(",", TokenKind.Comma)]
    [TestCase(".", TokenKind.Dot)]
    [TestCase("+", TokenKind.Plus)]
    [TestCase("-", TokenKind.Minus)]
    [TestCase("*", TokenKind.Asterisk)]
    [TestCase("/", TokenKind.Slash)]
    [TestCase("==", TokenKind.EqualsEquals)]
    [TestCase("!=", TokenKind.NotEquals)]
    [TestCase("=", TokenKind.Equals)]
    [TestCase("<", TokenKind.LessThan)]
    [TestCase("<=", TokenKind.LessThanOrEqual)]
    [TestCase(">", TokenKind.GreaterThan)]
    [TestCase(">=", TokenKind.GreaterThanOrEqual)]
    [TestCase("&&", TokenKind.AmpersandAmpersand)]
    [TestCase("||", TokenKind.PipePipe)]
    [TestCase("&", TokenKind.Ampersand)]
    [TestCase("|", TokenKind.Pipe)]
    public void GetTokensForOperatorTest(string code, TokenKind expectedTokenKind)
    {
        var lexer = new Lexer();
        var tokens = lexer.Tokenize(code);
        var expected = new[]
        {
            new Token(expectedTokenKind, null),
            Token.EndOfFile()
        };

        Assert.That(tokens, Is.EqualTo(expected));
    }
}