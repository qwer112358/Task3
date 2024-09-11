using Spectre.Console;
using System.Collections.Immutable;


public sealed class TableRenderer
{
    private readonly ImmutableList<string> _moves;
    private readonly string _headerLabel;
    private readonly string _headerColor;
    private readonly string _movesColor;
    private readonly GameRules _gameRules;
    private readonly Dictionary<GameResult, (string Color, string Label)> _resultsStyle;

    public TableRenderer(ImmutableList<string> moves)
    {
        _moves = moves;
        _headerLabel = "v PC/User >";
        _headerColor = "cyan";
        _movesColor = "cyan";
        _resultsStyle = new Dictionary<GameResult, (string Color, string Label)>
        {
            { GameResult.Draw, ("grey", "Draw") },
            { GameResult.PlayerWins, ("green", "Win") },
            { GameResult.ComputerWins, ("red", "Lose") }
        };
        _gameRules = GameRules.Instance;
    }

    public Table CreateTable(int currentRowPage, int currentColumnPage, int rowsPerPage, int columnsPerPage)
    {
        var table = new Table();
        AddHeader(table, currentColumnPage, columnsPerPage);
        AddRows(table, currentRowPage, currentColumnPage, rowsPerPage, columnsPerPage);
        return table;
        
    }

    private void AddHeader(Table table, int currentColumnPage, int columnsPerPage)
    {
        table.AddColumn(new TableColumn(CreateColoredText(_headerLabel, _headerColor)).Centered());
        var paginatedColumns = _moves.Skip(currentColumnPage * columnsPerPage).Take(columnsPerPage);
        foreach (var move in paginatedColumns)
        {
            table.AddColumn(new TableColumn(CreateColoredText(move, _movesColor)).Centered());
        }
    }

    private void AddRows(Table table, int currentRowPage, int currentColumnPage, int rowsPerPage, int columnsPerPage)
    {
        var paginatedMoves = _moves.Skip(currentRowPage * rowsPerPage).Take(rowsPerPage);
        foreach (var move in paginatedMoves.Select((move, rowIndex) => (move, rowIndex)))
        {
            var rowData = new List<string> { CreateColoredText(move.move, _movesColor) };
            var paginatedColumns = _moves.Skip(currentColumnPage * columnsPerPage).Take(columnsPerPage);
            rowData.AddRange(paginatedColumns.Select((_, colIndex) => GetFormattedResult(move.rowIndex + currentRowPage * rowsPerPage, colIndex + currentColumnPage * columnsPerPage)));
            table.AddRow(rowData.ToArray());
        }
    }

    private string GetFormattedResult(int row, int column)
    {
        GameResult result = _gameRules.GetGameResult(column, row);
        var (color, label) = _resultsStyle[result];
        return CreateColoredText(label, color);
    }

    private string CreateColoredText(string text, string color) =>
        $"[{color}]{text}[/]";
}