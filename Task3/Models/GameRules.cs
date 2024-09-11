using System.Collections.Immutable;

public sealed class GameRules
{
    private static readonly GameRules _instance = new GameRules(ImmutableList<string>.Empty);

    private ImmutableList<string> _moves;

    private GameRules(ImmutableList<string> moves)
    {
        _moves = moves;
    }

    public static GameRules Instance => _instance;

    public void SetMoves(ImmutableList<string> moves)
    {
        _moves = moves;
    }

    public GameResult GetGameResult(int playerMove, int computerMove)
    {
        if (playerMove == computerMove)
        {
            return GameResult.Draw;
        }
        if (PlayerWins(playerMove, computerMove))
        {
            return GameResult.PlayerWins;
        }
        return GameResult.ComputerWins;
    }

    private bool PlayerWins(int playerMove, int computerMove)
    {
        int halfSize = _moves.Count / 2;
        return (playerMove > computerMove && playerMove - computerMove <= halfSize)
            || (computerMove > playerMove && computerMove - playerMove > halfSize);
    }
}
