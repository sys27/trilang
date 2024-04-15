using System.Diagnostics.CodeAnalysis;
using Trilang.Lexing;
using Trilang.Parsing.Ast;

namespace Trilang.Parsing;

public class Parser
{
    private readonly TokenReader reader;

    public Parser(string code)
    {
        var lexer = new Lexer();
        var tokens = lexer.Tokenize(code);
        reader = new TokenReader(tokens);
    }

    public SyntaxTree Parse()
    {
        var functions = new List<FunctionDefinitionNode>();

        while (!reader.HasEnded)
        {
            var function = ParseFunctionDefinition();
            if (function is null)
                throw new Exception();

            functions.Add(function);
        }

        return new SyntaxTree(functions);
    }

    private FunctionDefinitionNode? ParseFunctionDefinition()
    {
        if (!reader.Check(TokenKind.Function))
            return null;

        var name = TryParseIdentifier();
        if (name is null)
            throw new Exception("Expected a function name.");

        var parameters = ParseFunctionParameters();
        var returnType = ParseFunctionReturnType();
        var block = ParseBlock();

        return new FunctionDefinitionNode(
            name,
            parameters,
            returnType,
            block);
    }

    private List<FunctionParameterNode> ParseFunctionParameters()
    {
        if (!reader.Check(TokenKind.OpenParenthesis))
            throw new Exception("Expected an open parenthesis.");

        var parameters = new List<FunctionParameterNode>();

        var parameter = TryParseFunctionParameter();
        if (parameter is not null)
        {
            parameters.Add(parameter);

            while (reader.Check(TokenKind.Comma))
            {
                parameter = TryParseFunctionParameter();
                if (parameter is null)
                    throw new Exception("Expected a parameter.");

                parameters.Add(parameter);
            }
        }

        if (!reader.Check(TokenKind.CloseParenthesis))
            throw new Exception("Expected an open parenthesis.");

        return parameters;
    }

    private FunctionParameterNode? TryParseFunctionParameter()
    {
        var name = TryParseIdentifier();
        if (name is null)
            return null;

        if (!reader.Check(TokenKind.Colon))
            throw new Exception("Expected a colon.");

        var type = TryParseIdentifier();
        if (type is null)
            throw new Exception("Expected a type.");

        return new FunctionParameterNode(name, type);
    }

    private IdentifierNode ParseFunctionReturnType()
    {
        if (!reader.Check(TokenKind.Colon))
            throw new Exception("Expected a colon.");

        var type = TryParseIdentifier();
        if (type is null)
            throw new Exception("Expected a type.");

        return type;
    }

    private BlockStatementNode ParseBlock()
    {
        if (!reader.Check(TokenKind.OpenBrace))
            throw new Exception("Expected an open brace.");

        var statements = new List<IStatementNode>();
        while (!reader.Check(TokenKind.CloseBrace))
        {
            var statement = ParseStatement();

            statements.Add(statement);
        }

        return new BlockStatementNode(statements);
    }

    private IStatementNode ParseStatement()
    {
        var statement = TryParseReturnStatement();
        if (statement is null)
            throw new Exception();

        return statement;
    }

    private ReturnStatementNode? TryParseReturnStatement()
    {
        if (!reader.Check(TokenKind.Return))
            return null;

        var expression = TryParseExpression();
        if (expression is null)
            throw new Exception("Expected an expression.");

        if (!reader.Check(TokenKind.Semicolon))
            throw new Exception("Expected a semicolon.");

        return new ReturnStatementNode(expression);
    }

    private IExpressionNode? TryParseExpression()
        => TryParseBinaryExpression();

    private IExpressionNode? TryParseBinaryExpression()
    {
        var left = TryParseOperand();
        if (left is null)
            return null;

        while (true)
        {
            BinaryOperatorKind kind;
            if (reader.Check(TokenKind.Plus))
                kind = BinaryOperatorKind.Add;
            else if (reader.Check(TokenKind.Minus))
                kind = BinaryOperatorKind.Subtract;
            else if (reader.Check(TokenKind.Asterisk))
                kind = BinaryOperatorKind.Multiply;
            else if (reader.Check(TokenKind.Slash))
                kind = BinaryOperatorKind.Divide;
            else
                break;

            var right = TryParseOperand();
            if (right is null)
                throw new Exception("Expected right operand.");

            left = new BinaryExpressionNode(left, right, kind);
        }

        return left;
    }

    private IExpressionNode? TryParseOperand()
        => TryParseLiteral() ??
           TryParseIdentifierExpression();

    private IExpressionNode? TryParseLiteral()
    {
        if (reader.Check(TokenKind.Number, out var token))
            return new LiteralExpressionNode(token.AsNumber());

        return null;
    }

    private IExpressionNode? TryParseIdentifierExpression()
    {
        if (reader.Check(TokenKind.Identifier, out var token))
            return new IdentifierExpressionNode(token.AsString());

        return null;
    }

    private IdentifierNode? TryParseIdentifier()
    {
        if (!reader.Check(TokenKind.Identifier, out var token))
            return null;

        return new IdentifierNode(token.AsString());
    }

    private sealed class TokenReader
    {
        private readonly List<Token> tokens;
        private int index;

        public TokenReader(List<Token> tokens)
        {
            this.tokens = tokens;
            index = 0;
        }

        private void Advance()
        {
            if (!HasEnded)
                index++;
        }

        public bool Check(TokenKind kind)
            => Check(kind, out _);

        public bool Check(TokenKind kind, [NotNullWhen(true)] out Token? token)
        {
            token = null;

            var result = Current.Is(kind);
            if (result)
            {
                token = Current;
                Advance();
            }

            return result;
        }

        public Token Current
            => tokens[index];

        public bool HasEnded
            => index >= tokens.Count || tokens[index].Is(TokenKind.EndOfFile);
    }
}