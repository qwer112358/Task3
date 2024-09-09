using System;
using System.Collections.Immutable;
using System.Linq;
using Spectre.Console;

public class HelpTable
{
    private readonly ImmutableList<string> _moves;
    private readonly string _headerLabel;
    private readonly string _headerColor;
    private readonly string _movesColor;
    private readonly Dictionary<GameResult, (string Color, string Label)> _resultsStyle;
    private Table _table;

    public HelpTable(ImmutableList<string> moves)
    {
        _moves = moves;
        _table = new Table();
        _headerLabel = "v PC/User >";
        _movesColor = "cyan";
        _headerColor = "cyan";
        _resultsStyle = new Dictionary<GameResult, (string Color, string Label)>()
        {
            { GameResult.Draw, ("grey", "Draw") },
            { GameResult.PlayerWins, ("green", "Win") },
            { GameResult.ComputerWins, ("red", "Lose") },
        };
    }

    public void PrintHelpTable()
    {
        AddHeader();
        AddRows();
        AnsiConsole.Write(_table);
    }

    private void AddHeader()
    {
        _table.AddColumn(new TableColumn($"[{_headerColor}]{_headerLabel}[/]").Centered());
        _moves.ForEach(move => _table.AddColumn(new TableColumn($"[{_movesColor}]{move}[/]").Centered()));
    }

    private void AddRows() =>
        _moves.Select((move, rowIndex) =>
        {
            var rowData = new List<string> { $"[{_movesColor}]{move}[/]" };
            rowData.AddRange(_moves.Select((_, colIndex) => GetFormattedResult(rowIndex, colIndex)));
            return rowData.ToArray();
        }).ToList().ForEach(rowData => _table.AddRow(rowData));

    private string GetFormattedResult(int row, int column)
    {
        GameResult result = new GameRules(_moves).GetGameResult(column, row);
        var (color, label) = _resultsStyle[result];
        return $"[{color}]{label}[/]";
    }
}
