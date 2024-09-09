using System.Collections.Immutable;

public sealed class MoveDisplay
{
    private readonly ImmutableList<string> _moves;

    public MoveDisplay(ImmutableList<string> moves)
    {
        _moves = moves;
    }

    public void ShowAvailableMoves()
    {
        Console.WriteLine("Available moves:");

        for (int index = 0; index < _moves.Count; index++)
        {
            Console.WriteLine($"{index + 1} - {_moves[index]}");
        }

        Console.WriteLine("0 - Exit");
        Console.WriteLine("? - Help");
    }

    public void DisplayInvalidInputMessage() =>
        Console.WriteLine("Invalid input. Try again.");

    public void DisplayHelp()
    {
        var helpTable = new HelpTable(_moves);
        helpTable.PrintHelpTable();
    }
}
