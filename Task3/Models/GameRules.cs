using System;
using System.Collections.Generic;
using System.Collections.Immutable;

public class GameRules
{
    protected ImmutableList<string> Moves { get; }
    
    public GameRules(ImmutableList<string> moves)
    {
        Moves = moves;
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
        int halfSize = Moves.Count / 2;
        return (playerMove > computerMove && playerMove - computerMove <= halfSize)
            || (computerMove > playerMove && computerMove - playerMove > halfSize);
    }
}
