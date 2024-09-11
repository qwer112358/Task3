public sealed class Pagination
{
    private readonly int _totalRows;
    private readonly int _totalColumns;

    public int RowsPerPage { get; init; }
    public int CurrentRowPage { get; private set; }
    public int CurrentColumnPage { get; private set; }
    public int ColumnsPerPage { get; init; }
    public int TotalRowPages { get; init; }
    public int TotalColumnPages { get; init; }

    public Pagination(int rowsPerPage, int columnsPerPage, int totalRows, int totalColumns)
    {
        RowsPerPage = rowsPerPage;
        ColumnsPerPage = columnsPerPage;
        _totalRows = totalRows;
        _totalColumns = totalColumns;
        CurrentRowPage = 0;
        CurrentColumnPage = 0;
        TotalRowPages = IntegerCeiling(totalRows, rowsPerPage);
        TotalColumnPages = IntegerCeiling(totalColumns, columnsPerPage);
    }

    private int IntegerCeiling(int x, int y) => (x + y - 1) / y;
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

    public (int RowPage, int ColumnPage) GetCurrentPages() => (CurrentRowPage, CurrentColumnPage);

    private bool MoveUp()
    {
        if (CurrentRowPage > 0)
            CurrentRowPage--;
        return true;
    }

    private bool MoveDown()
    {
        if (CurrentRowPage < (_totalRows - 1) / RowsPerPage)
            CurrentRowPage++;
        return true;
    }

    private bool MoveLeft()
    {
        if (CurrentColumnPage > 0)
            CurrentColumnPage--;
        return true;
    }

    private bool MoveRight()
    {
        if (CurrentColumnPage < (_totalColumns - 1) / ColumnsPerPage)
            CurrentColumnPage++;
        return true;
    }
}
