using System;
using System.Collections.Immutable;

public sealed class Game
{
    private readonly ImmutableList<string> _moves;
    private readonly MoveGenerator _moveGenerator;
    private readonly HmacHandler _hmacHandler;
    private readonly MoveDisplay _moveDisplay;
    private readonly GameRules _gameRules;
    private readonly GameResultPrinter _gameResultPrinter;
    private bool _isGameRunning;

    public Game(ImmutableList<string> moves)
    {
        _moves = moves;
        _moveGenerator = new MoveGenerator(moves);
        _hmacHandler = new HmacHandler();
        _moveDisplay = new MoveDisplay(moves);
        _gameRules = GameRules.Instance;
        _gameRules.SetMoves(moves);
        _gameResultPrinter = new GameResultPrinter();
    }

    public void Start()
    {
        _isGameRunning = true;
        InitializeGame();

        while (_isGameRunning)
        {
            var input = ReadInput();
            if (input == null || ProcessInput(input)) continue;

            HandlePlayerMove(input);
        }
    }

    private void InitializeGame()
    {
        _hmacHandler.GenerateNewKey();
        _moveGenerator.GenerateComputerMove();
        _hmacHandler.DisplayHmac(_moveGenerator.ComputerMove);
        _moveDisplay.ShowAvailableMoves();
    }

    private string? ReadInput()
    {
        Console.Write("Enter your move: ");
        return Console.ReadLine();
    }

    private bool ProcessInput(string? input) =>
        input switch
        {
            "0" => ExitGame(),
            "?" => DisplayHelp(),
            _ => false
        };

    private bool ExitGame()
    {
        Console.WriteLine("Game exited.");
        _isGameRunning = false;
        return true;
    }

    private bool DisplayHelp()
    {
        _moveDisplay.DisplayHelp();
        return true;
    }

    private void HandlePlayerMove(string input)
    {
        if (TryParsePlayerMove(input, out int playerMoveIndex))
        {
            PlayRound(playerMoveIndex - 1);
        }
        else
        {
            _moveDisplay.DisplayInvalidInputMessage();
        }
    }

    private bool TryParsePlayerMove(string input, out int playerMoveIndex) =>
        int.TryParse(input, out playerMoveIndex) && playerMoveIndex > 0 && playerMoveIndex <= _moves.Count;

    private void PlayRound(int playerMoveIndex)
    {
        var playerMove = _moves[playerMoveIndex];
        var computerMove = _moveGenerator.ComputerMove;
        var result = _gameRules.GetGameResult(playerMoveIndex, _moveGenerator.ComputerMoveIndex);

        PrintRoundResults(playerMove, computerMove, result);
        PrintHmacInfo();

        Console.WriteLine("\n" + new string('-', 40));
        Console.WriteLine("Next round:");
        InitializeGame();
    }

    private void PrintRoundResults(string playerMove, string computerMove, GameResult result)
    {
        Console.WriteLine($"Your move: {playerMove}");
        Console.WriteLine($"Computer move: {computerMove}");
        _gameResultPrinter.PrintResult(result);
    }

    private void PrintHmacInfo()
    {
        Console.WriteLine($"HMAC key: {_hmacHandler.Key}");
        Console.WriteLine("Verify HMAC using an online service:");
        Console.WriteLine(_hmacHandler.GenerateHmacVerificationUrl(_moveGenerator.ComputerMove));
        Console.WriteLine();
    }
}
