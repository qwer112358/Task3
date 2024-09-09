using System.Collections.Immutable;

public class MoveGenerator
{
    private readonly ImmutableList<string> _moves;
    private readonly Random _random;
    public string ComputerMove { get; private set; } = string.Empty;
    public int ComputerMoveIndex { get; private set; }

    public MoveGenerator(ImmutableList<string> moves)
    {
        _moves = moves;
        _random = new Random();
    }

    public void GenerateComputerMove()
    {
        ComputerMoveIndex = _random.Next(_moves.Count);
        ComputerMove = _moves[ComputerMoveIndex];
    }
}
