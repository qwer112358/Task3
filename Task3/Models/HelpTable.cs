using System;
using System.Collections.Immutable;
using System.Linq;
using Spectre.Console;

public sealed class HelpTable
{
    private readonly ImmutableList<string> _moves;
    private readonly string _headerLabel = "v PC/User >";
    private readonly string _headerColor = "cyan";
    private readonly string _movesColor = "cyan";
    private readonly Dictionary<GameResult, (string Color, string Label)> _resultsStyle;
    private Table _table;

    public HelpTable(ImmutableList<string> moves)
    {
        _moves = moves;
        _table = new Table();
        _headerLabel = "v PC/User >";
        _headerColor = "cyan";
        _movesColor = "cyan";
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
        _table.AddColumn(new TableColumn(CreateColoredText(_headerLabel, _headerColor)).Centered());
        _moves.ForEach(move => _table.AddColumn(new TableColumn(CreateColoredText(move, _movesColor)).Centered()));
    }

    private void AddRows() =>
        _moves.Select((move, rowIndex) =>
        {
            var rowData = new List<string> { CreateColoredText(move, _movesColor) };
            rowData.AddRange(_moves.Select((_, colIndex) => GetFormattedResult(rowIndex, colIndex)));
            return rowData.ToArray();
        }).ToList().ForEach(rowData => _table.AddRow(rowData));

    private string GetFormattedResult(int row, int column)
    {
        GameResult result = new GameRules(_moves).GetGameResult(column, row);
        var (color, label) = _resultsStyle[result];
        return CreateColoredText(label, color);
    }

    private string CreateColoredText(string text, string color) =>
        $"[{color}]{text}[/]";
}
