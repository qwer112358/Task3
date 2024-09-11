using System;
using System.Collections.Immutable;
using System.Linq;
using Spectre.Console;
/*
public sealed class HelpTable
{
    private readonly ImmutableList<string> _moves;
    private readonly string _headerLabel;
    private readonly string _headerColor;
    private readonly string _movesColor;
    private readonly Dictionary<GameResult, (string Color, string Label)> _resultsStyle;
    private Table _table;
    private int RowsPerPage;
    private int ColumnsPerPage;
    private int _currentRowPage;
    private int _currentColumnPage;

    public HelpTable(ImmutableList<string> moves, int rowsPerPage = 10, int columnsPerPage = 10)
    {
        _moves = moves;
        _headerLabel = "v PC/User >";
        _headerColor = "cyan";
        _movesColor = "cyan";
        _resultsStyle = new Dictionary<GameResult, (string Color, string Label)>()
        {
            { GameResult.Draw, ("grey", "Draw") },
            { GameResult.PlayerWins, ("green", "Win") },
            { GameResult.ComputerWins, ("red", "Lose") },
        };
        RowsPerPage = rowsPerPage;
        ColumnsPerPage = columnsPerPage;
        _currentRowPage = 0;
        _currentColumnPage = 0;
        _table = new Table();
    }

    public void PrintHelpTable()
    {
        while (true)
        {
            Console.Clear();
            PrintTableDescription();
            _table = new Table();
            AddHeader();
            AddRows();
            AnsiConsole.Write(_table);

            if (!DisplayNextPageOptions())
                break;
        }
    }

    private void AddHeader()
    {
        _table.AddColumn(new TableColumn(CreateColoredText(_headerLabel, _headerColor)).Centered());
        var paginatedColumns = _moves.Skip(_currentColumnPage * ColumnsPerPage).Take(ColumnsPerPage);
        foreach (var move in paginatedColumns)
        {
            _table.AddColumn(new TableColumn(CreateColoredText(move, _movesColor)).Centered());

        }
    }

    private void AddRows()
    {
        var paginatedMoves = _moves.Skip(_currentRowPage * RowsPerPage).Take(RowsPerPage);
        foreach (var move in paginatedMoves.Select((move, rowIndex) => (move, rowIndex)))
        {
            var rowData = new List<string> { CreateColoredText(move.move, _movesColor) };
            var paginatedColumns = _moves.Skip(_currentColumnPage * ColumnsPerPage).Take(ColumnsPerPage);
            rowData.AddRange(paginatedColumns.Select((_, colIndex) => GetFormattedResult(move.rowIndex + _currentRowPage * RowsPerPage, colIndex + _currentColumnPage * ColumnsPerPage)));
            _table.AddRow(rowData.ToArray());
        }
    }

    private string GetFormattedResult(int row, int column)
    {
        GameResult result = GameRules.Instance.GetGameResult(column, row);
        var (color, label) = _resultsStyle[result];
        return CreateColoredText(label, color);
    }

    private string CreateColoredText(string text, string color) =>
        $"[{color}]{text}[/]";

    private bool DisplayNextPageOptions()
    {
        var totalRowPages = (int)Math.Ceiling((double)_moves.Count / RowsPerPage);
        var totalColumnPages = (int)Math.Ceiling((double)_moves.Count / ColumnsPerPage);

        Console.WriteLine($"Row Page {_currentRowPage + 1}/{totalRowPages} | Column Page {_currentColumnPage + 1}/{totalColumnPages}");
        Console.WriteLine("Use arrow keys (↑ ↓) or W/S (up/down) for rows, (← →) or A/D (left/right) for columns. Press Q to quit.");

        var key = Console.ReadKey(true).Key;
        return HandleKeyPress(key, totalRowPages, totalColumnPages);
    }

    private bool HandleKeyPress(ConsoleKey key, int totalRowPages, int totalColumnPages)
    {
        return key switch
        {
            ConsoleKey.W or ConsoleKey.UpArrow => MoveUp(totalRowPages),
            ConsoleKey.S or ConsoleKey.DownArrow => MoveDown(totalRowPages),
            ConsoleKey.A or ConsoleKey.LeftArrow => MoveLeft(totalColumnPages),
            ConsoleKey.D or ConsoleKey.RightArrow => MoveRight(totalColumnPages),
            ConsoleKey.Q => false,
            _ => true,
        };
    }

    private bool MoveUp(int totalRowPages)
    {
        if (_currentRowPage > 0)
            _currentRowPage--;
        return true;
    }

    private bool MoveDown(int totalRowPages)
    {
        if (_currentRowPage < totalRowPages - 1)
            _currentRowPage++;
        return true;
    }

    private bool MoveLeft(int totalColumnPages)
    {
        if (_currentColumnPage > 0)
            _currentColumnPage--;
        return true;
    }

    private bool MoveRight(int totalColumnPages)
    {
        if (_currentColumnPage < totalColumnPages - 1)
            _currentColumnPage++;
        return true;
    }

    private void PrintTableDescription()
    {
        Console.WriteLine("This table shows the results of the game from the perspective of the user.");
        Console.WriteLine("The results are presented as follows:");
        Console.WriteLine("- Draw: The game ended in a draw.");
        Console.WriteLine("- Win: The user won the game.");
        Console.WriteLine("- Lose: The user lost the game.");
        Console.WriteLine();
    }
}
*/

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
        Console.WriteLine("- Lose: The user lost the game.");
        Console.WriteLine();
    }
}
