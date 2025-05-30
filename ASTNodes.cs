using LFA.Labs.Lexer;

public abstract class ASTNode
{
    public virtual void Accept(IASTVisitor visitor) { }
}

public interface IASTVisitor
{
    void Visit(ProgramNode node);
    void Visit(BlockNode node);
    void Visit(LiteralNode node);
}

public class ProgramNode : ASTNode
{
    public List<BlockNode> Blocks { get; } = new();
    public override void Accept(IASTVisitor visitor) => visitor.Visit(this);
}

public class BlockNode : ASTNode
{
    public List<ASTNode> Statements { get; } = new();
    public override void Accept(IASTVisitor visitor) => visitor.Visit(this);
}

public class LiteralNode : ASTNode
{
    public TokenType Type { get; set; }
    public string Value { get; set; }
    public override void Accept(IASTVisitor visitor) => visitor.Visit(this);
}