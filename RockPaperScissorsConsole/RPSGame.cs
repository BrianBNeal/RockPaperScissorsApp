using System.Text;

namespace RockPaperScissorsConsole;

/// <summary>
/// A game of Rock, Paper, Scissors. Enjoy!
/// </summary>
public class RPSGame
{
    private ScoreCard _sc;
    private Random _picker;
    private RPSOption _currentChoice;
    private RPSOption _computerChoice;
    private readonly string[] _validAffirmatives = new string[] { "y", "yes" };
    private readonly string[] _validNegatives = new string[] { "n", "no" };
    private readonly Dictionary<RPSOption, string> _graphics = new Dictionary<RPSOption, string>()
    {
        { RPSOption.None, "" },
        { RPSOption.Rock, @"
    _______
---'   ____)
      (_____)
      (_____)
      (____)
---.__(___)" },
        { RPSOption.Paper, @"
     _______
---'    ____)____
           ______)
          _______)
         _______)
---.__________)" },
        { RPSOption.Scissors, @"
    _______
---'   ____)____
          ______)
       __________)
      (____)
---.__(___)" }
    };

    //this is the constructor for this class, this is code that executes when an instance of RPSGame is created with 'new RPSGame()'
    public RPSGame()
    {
        _sc = new ScoreCard();
        _picker = new Random();
        _currentChoice = RPSOption.None;
        _computerChoice = RPSOption.None;
    }

    /// <summary>
    /// Initiates a game of RPS
    /// </summary>
    public void Run()
    {
        bool playing = true;

        while (playing)
        {
            GetCurrentChoice();
            DisplayChoices();
            ResolveRound();
            playing = AskToContinue();
        }

        SayGoodbye();
    }

    /// <summary>
    /// Displays graphics for the player's choice as well as the computer's choice (chosen at random)
    /// </summary>
    private void DisplayChoices()
    {
        Console.WriteLine();
        Console.WriteLine($"You picked {Enum.GetName(_currentChoice)}!");

        _computerChoice = (RPSOption)_picker.Next(1, 4);
        
        Console.WriteLine();
        Pause();
        Console.Write("...3");
        Pause();
        Console.Write("...2");
        Pause();
        Console.Write("...1");
        Pause();
        Console.WriteLine("...Here we go!!!");
        Pause();
        Console.WriteLine();
        Console.WriteLine($"{_graphics[_currentChoice]}   VS   {_graphics[_computerChoice]}");
        Pause(2.4);
    }

    /// <summary>
    /// Pauses for the designated period of time to create a better flow
    /// </summary>
    /// <param name="duration">The amount of time in seconds for which you wish to pause.</param>
    private void Pause(double duration = .8)
    {
        Task.Delay(TimeSpan.FromSeconds(duration)).Wait();
    }

    /// <summary>
    /// Asks for user input and determines if another round should be played.
    /// </summary>
    /// <returns>True if the player responds affirmatively, false if the player responds negatively.</returns>
    private bool AskToContinue()
    {
        string? input = "";

        Console.WriteLine();
        while (String.IsNullOrWhiteSpace(input) || !ValidYesNoResponse(input))
        {
            Console.Write("Would you like to play again (y/n)? ");
            input = Console.ReadLine();
        }

        return _validAffirmatives.Contains(input.ToLower());
    }

    /// <summary>
    /// Validates user input to determine if it is a valid yes/no response.
    /// </summary>
    /// <param name="input"></param>
    /// <returns>True if the input is included in the list of valid responses, false if not included in the list of valid responses.</returns>
    private bool ValidYesNoResponse(string input)
    {
        return _validAffirmatives.Contains(input) || _validNegatives.Contains(input);
    }

    /// <summary>
    /// Displays a parting message and the final score.
    /// </summary>
    private void SayGoodbye()
    {
        Console.WriteLine();
        Console.WriteLine("Thanks for playing!");
        Console.WriteLine();
        Console.Write("Press any key to end...");
        Console.ReadKey();
    }

    /// <summary>
    /// Prints the result of a round along with the score.
    /// </summary>
    /// <param name="result"></param>
    private void ShowResult(RPSResult result)
    {
        string resultText = $"It was a {Enum.GetName(result)}!";

        Console.WriteLine();
        Console.WriteLine(resultText);
        Console.WriteLine(_sc.ScoreBoard);
    }

    /// <summary>
    /// Calculates whether a round was a Win, Loss, or Draw and shows the score.
    /// </summary>
    private void ResolveRound()
    {
        RPSResult roundResult = RPSResult.Draw;

        switch (_currentChoice)
        {
            case RPSOption.Rock when _computerChoice == RPSOption.Paper:
            case RPSOption.Paper when _computerChoice == RPSOption.Scissors:
            case RPSOption.Scissors when _computerChoice == RPSOption.Rock:
                roundResult = RPSResult.Loss;
                break;

            case RPSOption.Rock when _computerChoice == RPSOption.Scissors:
            case RPSOption.Paper when _computerChoice == RPSOption.Rock:
            case RPSOption.Scissors when _computerChoice == RPSOption.Paper:
                roundResult = RPSResult.Win;
                break;

            default:
                break;
        }

        _sc.RecordResult(roundResult);
        ShowResult(roundResult);
    }

    /// <summary>
    /// Asks the user for their choice in a game of RPS and validates the input
    /// </summary>
    private void GetCurrentChoice()
    {
        _currentChoice = RPSOption.None;

        while (_currentChoice == RPSOption.None)
        {
            Console.Clear();
            Console.WriteLine(_mainBanner);
            Console.WriteLine(OptionsText());
            Console.Write("What's your choice? ");
            string input = Console.ReadKey().KeyChar.ToString(); //I thought a single button press would be nice
            if (int.TryParse(input, out int inputAsInt) && inputAsInt < 4) //I'm checking to make sure a number was pressed, and one less than 4 (the valid values for RPSOption)
            {
                _currentChoice = (RPSOption)inputAsInt;
            }
        }
    }

    /// <summary>
    /// Creates numbered menu options from the values of the RPSOption enum
    /// </summary>
    /// <returns>A string representation of the RPSOptions enum as menu options.</returns>
    private string OptionsText()
    {
        StringBuilder builder = new StringBuilder();

        foreach (var option in Enum.GetValues<RPSOption>())
        {
            if (option == 0) { continue; }//this skips the 0 enum option so it won't be in the menu
            builder.AppendLine($"{(int)option}. {Enum.GetName(option)}");
        }

        return builder.ToString();
    }
}
