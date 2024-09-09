using System.Collections.Immutable;

public class MoveDisplay
{
    private readonly ImmutableList<string> _moves;
    private readonly HelpTable _helpTable;

    public MoveDisplay(ImmutableList<string> moves)
    {
        _moves = moves;
        _helpTable = new HelpTable(_moves);
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

    public void DisplayHelp() => _helpTable.PrintHelpTable();
}
