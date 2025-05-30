using System.Text;

public class ASTPrinter : IASTVisitor
{
    private readonly StringBuilder _sb = new();
    private int _indent = 0;

    public string Print(ASTNode node)
    {
        _sb.Clear();
        _indent = 0;
        node.Accept(this);
        return _sb.ToString();
    }

    private void AppendIndent() => _sb.Append(new string(' ', _indent * 2));
    private void IncreaseIndent() => _indent++;
    private void DecreaseIndent() => _indent = Math.Max(0, _indent - 1);

    public void Visit(ProgramNode node)
    {
        AppendIndent();
        _sb.AppendLine("Program:");
        IncreaseIndent();
        foreach (var block in node.Blocks)
            block.Accept(this);
        DecreaseIndent();
    }

    public void Visit(BlockNode node)
    {
        AppendIndent();
        _sb.AppendLine("Block:");
        IncreaseIndent();
        foreach (var stmt in node.Statements)
            stmt.Accept(this);
        DecreaseIndent();
    }

    public void Visit(LiteralNode node)
    {
        AppendIndent();
        _sb.AppendLine($"Literal: {node.Type} => '{node.Value}'");
    }
}