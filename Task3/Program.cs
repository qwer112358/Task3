using System;
using System.Collections.Immutable;

sealed class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 3 || args.Length % 2 == 0)
        {
            Console.WriteLine("Error: You must provide an odd number of moves (≥ 3). Example: rock paper scissors");
            return;
        }
        ImmutableList<string> moves = args.ToImmutableList();
        if (new HashSet<string>(moves).Count != moves.Count)
        {
            Console.WriteLine("Error: Moves must be unique.");
            return;
        }
        Game game = new Game(moves);
        game.Start();
    }
}
