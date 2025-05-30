using lab3.Lexer;
using LFA.Labs.Lexer;

public class Parser
{
    private readonly List<Token> _tokens;
    private int _position;
    private Token Current => _position < _tokens.Count ? _tokens[_position] : null;

    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
        _position = 0;
    }

    public ProgramNode Parse()
    {
        var program = new ProgramNode();
        var block = new BlockNode();

        while (Current != null && Current.Type != TokenType.EOF)
        {
            block.Statements.Add(new LiteralNode
            {
                Type = Current.Type,
                Value = Current.Value
            });
            _position++;
        }

        program.Blocks.Add(block);
        return program;
    }
}