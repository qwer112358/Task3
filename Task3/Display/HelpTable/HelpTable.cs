using System.Collections.Immutable;
using Spectre.Console;

public sealed class HelpTable
{
    private readonly TableRenderer _tableRenderer;
    private readonly Pagination _pagination;

    public HelpTable(ImmutableList<string> moves, int rowsPerPage = 10, int columnsPerPage = 10)
    {
        _tableRenderer = new TableRenderer(moves);
        _pagination = new Pagination(rowsPerPage, columnsPerPage, moves.Count, moves.Count);
    }

    public void PrintHelpTable()
    {
        while (true)
        {
            Console.Clear();
            PrintTableDescription();
            var (currentRowPage, currentColumnPage) = _pagination.GetCurrentPages();
            var table = _tableRenderer.CreateTable(currentRowPage, currentColumnPage, _pagination.RowsPerPage, _pagination.ColumnsPerPage);
            AnsiConsole.Write(table);
            Console.WriteLine($"Row Page {_pagination.CurrentRowPage + 1}/{_pagination.TotalRowPages} " +
                $"| Column Page {_pagination.CurrentColumnPage + 1}/{_pagination.TotalColumnPages}");
            Console.WriteLine("Use arrow keys (↑ ↓) or W/S (up/down) for rows, (← →) or A/D (left/right) for columns. Press Q to quit.");
            if (!HandleInput())
                break;
        }
    }

    private bool HandleInput()
    {
        var key = Console.ReadKey(true).Key;
        return _pagination.HandleInput(key);
    }

    private void PrintTableDescription()
    {
        Console.WriteLine("This table shows the results of the game from the perspective of the user.");
        Console.WriteLine("The results are presented as follows:");
        Console.WriteLine("- Draw: The game ended in a draw.");
        Console.WriteLine("- Win: The user won the game.");
        Console.WriteLine("- Lose: The user lost the game.\n");
    }
}
