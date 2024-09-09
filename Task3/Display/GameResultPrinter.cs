using System;

public sealed class GameResultPrinter
{
    public void PrintResult(GameResult result)
    {
        string resultMessage = result switch
        {
            GameResult.Draw => "Draw!",
            GameResult.PlayerWins => "You win!",
            _ => "You lose!"
        };
        Console.WriteLine(resultMessage);
    }
}
