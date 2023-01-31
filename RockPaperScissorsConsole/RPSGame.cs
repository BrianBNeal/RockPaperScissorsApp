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
    private RPSResult _roundResult;
    private readonly string[] _validAffirmatives = new string[] { "y", "yes", "ok", "okay", "sure", "yep", "fine", "k", "yah" };
    private readonly string[] _validNegatives = new string[] { "n", "no", "nah", "nope", "negative", "negatory", "hell naw" };
    private readonly Dictionary<RPSOption, string> _graphics = new Dictionary<RPSOption, string>()
    {
        { RPSOption.None, "" },
        { RPSOption.Rock, @"
    _______  
---'   ____) 
      (_____)
      (_____)
      (____) 
---.__(___)  
" },
        { RPSOption.Paper, @"
     _______      
---'    ____)____ 
           ______)
          _______)
         _______) 
---.__________)   
" },
        { RPSOption.Scissors, @"
    _______       
---'   ____)____  
          ______) 
       __________)
      (____)      
---.__(___)       
" }
    };

    public RPSGame()
    {
        _sc = new ScoreCard();
        _picker = new Random();
        _currentChoice = RPSOption.None;
        _computerChoice = RPSOption.None;
        _roundResult = RPSResult.Draw;
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
            ShowResult();
            //playing = AskToContinueShort();
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
        Console.WriteLine($" You picked {Enum.GetName(_currentChoice)}!");

        _computerChoice = (RPSOption)_picker.Next(1, 4);

        Console.WriteLine();
        Pause();
        Console.Write(" ...3");
        Pause();
        Console.Write("...2");
        Pause();
        Console.Write("...1");
        Pause();
        Console.WriteLine("...Here we go!!!");
        Pause();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine(GetSideBySideGraphic());
        //Console.WriteLine(@$" {_graphics[_currentChoice]}  VS  {_graphics[_computerChoice]}");
        Pause(1.2);
    }

    /// <summary>
    /// Puts the graphics side by side to reduce scrolling
    /// </summary>
    /// <returns>A string representation of the graphics for this round.</returns>
    private string GetSideBySideGraphic()
    {
        var bldr = new StringBuilder();

        var playerSplit = _graphics[_currentChoice].Split("\r\n").Skip(1).Take(6).ToList();
        var compSplit = _graphics[_computerChoice].Split("\r\n").Skip(1).Take(6).ToList();

        for (int i = 0; i < playerSplit.Count; i++)
        {
            var padding = (i == 3) ? "    VS    " : "          ";
            bldr.AppendLine($"{playerSplit[i]}{padding}{ReverseHand(compSplit[i]).ToString()}");
        }

        return bldr.ToString();
    }

    /// <summary>
    /// Make the hands point towards each other!!!!!!!!!!!
    /// </summary>
    /// <param name="value">A string hand graphic that will be reversed left to right</param>
    /// <returns>A string for your graphics</returns>
    private string ReverseHand(string value)
    {
        char[] charArray = value.ToCharArray();
        Array.Reverse(charArray);
        var newResult = new string(charArray);
        return newResult.Replace("(", "`").Replace(")", "(").Replace("`", ")");

    }

    /// <summary>
    /// Pauses for the designated period of time to create a better flow
    /// </summary>
    /// <param name="duration">The amount of time in seconds for which you wish to pause.</param>
    private void Pause(double duration = .5)
    {
        Task.Delay(TimeSpan.FromSeconds(duration)).Wait();
    }

    private bool AskToContinueShort()
    {
        string? input = "";

        Console.WriteLine();
        Console.Write("Keep playing? (y/n)");

        while (String.IsNullOrWhiteSpace(input) || (input != "y" && input != "n"))
        {
            input = Console.ReadKey(true).KeyChar.ToString().ToLower();
        }

        return input == "y";
    }

    /// <summary>
    /// Asks for user input and determines if another round should be played.
    /// </summary>
    /// <returns>True if the player responds affirmatively, false if the player responds negatively.</returns>
    private bool AskToContinue()
    {
        string? input = "";

        Console.WriteLine();
        while (!ValidYesNoResponse(input))
        {
            Console.Write("Keep playing? ");
            input = Console.ReadLine()?.Trim().ToLower();
        }

        return _validAffirmatives.Contains(input);
    }

    private bool ValidYesNoResponse(string input)
    {
        return !string.IsNullOrWhiteSpace(input) && (_validAffirmatives.Contains(input) || _validNegatives.Contains(input));
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
        Console.ReadKey(true);
    }

    /// <summary>
    /// Prints the result of a round along with the score.
    /// </summary>
    private void ShowResult()
    {
        Console.WriteLine();
        Console.WriteLine($"It was a {Enum.GetName(_roundResult)}!");
    }

    /// <summary>
    /// Calculates whether a round was a Win, Loss, or Draw and shows the score.
    /// </summary>
    private void ResolveRound()
    {
        switch (_currentChoice)
        {
            case RPSOption.Rock when _computerChoice == RPSOption.Paper:
            case RPSOption.Paper when _computerChoice == RPSOption.Scissors:
            case RPSOption.Scissors when _computerChoice == RPSOption.Rock:
                _roundResult = RPSResult.Loss;
                break;

            case RPSOption.Rock when _computerChoice == RPSOption.Scissors:
            case RPSOption.Paper when _computerChoice == RPSOption.Rock:
            case RPSOption.Scissors when _computerChoice == RPSOption.Paper:
                _roundResult = RPSResult.Win;
                break;

            default:
                _roundResult = RPSResult.Draw;
                break;
        }

        _sc.RecordResult(_roundResult);
        _currentChoice = RPSOption.None;
    }

    /// <summary>
    /// Asks the user for their choice in a game of RPS and validates the input
    /// </summary>
    private void GetCurrentChoice()
    {
        Console.Clear();
        Console.WriteLine(_sc);
        Console.WriteLine(OptionsText());
        Console.Write(" What's your choice? ");

        while (_currentChoice == RPSOption.None)
        {
            string input = Console.ReadKey(true).KeyChar.ToString(); //I thought a single button press would be nice, that's why this is more complex than a ReadLine
            if (int.TryParse(input, out int inputAsInt) && inputAsInt < 4) //I'm checking to make sure a number was pressed, and one less than 4 (the valid values for RPSOption)
            {
                Console.Write(input);
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
            builder.AppendLine($" {(int)option}. {Enum.GetName(option)}");
        }

        return builder.ToString();
    }
}
