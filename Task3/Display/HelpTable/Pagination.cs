public sealed class Pagination
{
    private int _currentRowPage;
    private int _currentColumnPage;
    private readonly int _totalRows;
    private readonly int _totalColumns;

    public int RowsPerPage { get; init; }
    public int ColumnsPerPage { get; init; }

    public Pagination(int rowsPerPage, int columnsPerPage, int totalRows, int totalColumns)
    {
        RowsPerPage = rowsPerPage;
        ColumnsPerPage = columnsPerPage;
        _totalRows = totalRows;
        _totalColumns = totalColumns;
        _currentRowPage = 0;
        _currentColumnPage = 0;
    }

    public bool HandleInput(ConsoleKey key)
    {
        return key switch
        {
            ConsoleKey.W or ConsoleKey.UpArrow => MoveUp(),
            ConsoleKey.S or ConsoleKey.DownArrow => MoveDown(),
            ConsoleKey.A or ConsoleKey.LeftArrow => MoveLeft(),
            ConsoleKey.D or ConsoleKey.RightArrow => MoveRight(),
            ConsoleKey.Q => false,
            _ => true,
        };
    }

    public (int RowPage, int ColumnPage) GetCurrentPages() => (_currentRowPage, _currentColumnPage);

    private bool MoveUp()
    {
        if (_currentRowPage > 0)
            _currentRowPage--;
        return true;
    }

    private bool MoveDown()
    {
        if (_currentRowPage < (_totalRows - 1) / RowsPerPage)
            _currentRowPage++;
        return true;
    }

    private bool MoveLeft()
    {
        if (_currentColumnPage > 0)
            _currentColumnPage--;
        return true;
    }

    private bool MoveRight()
    {
        if (_currentColumnPage < (_totalColumns - 1) / ColumnsPerPage)
            _currentColumnPage++;
        return true;
    }
}