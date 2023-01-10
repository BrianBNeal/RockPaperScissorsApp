using System.Text;

namespace RockPaperScissorsConsole;

public class RPSGame
{
    private ScoreCard _score;


    private enum RPSOption
    {
        None,
        Rock,
        Paper,
        Scissors
    }

    private Dictionary<RPSOption, string> _graphics = new Dictionary<RPSOption, string>()
    {
        { RPSOption.None, "None" },
        { RPSOption.Rock, "Rock" },
        { RPSOption.Paper, "Paper" },
        { RPSOption.Scissors, "Scissors" }
    };

    private string _mainBanner = @"
       *************************
       *  Rock-Paper-Scissors  *
       *************************
";

    public void Run()
    {
        bool playing = true;

        while (playing)
        {
            GetUserChoice();
            ResolveRound();
            DetermineContinue();
        }

        SayGoodbye();
    }

    private void SayGoodbye()
    {
        throw new NotImplementedException();
    }

    private void DetermineContinue()
    {
        throw new NotImplementedException();
    }

    private void ResolveRound()
    {
        throw new NotImplementedException();
    }

    private void GetUserChoice()
    {
        throw new NotImplementedException();
    }

    private string OptionsText()
    {
        StringBuilder builder = new StringBuilder();

        foreach (var option in Enum.GetValues<RPSOption>())
        {
            builder.AppendLine($"{(int)option}. {Enum.GetName(option)}");
        }

        return builder.ToString();
    }

    private void Show(params string[] text)
    {
        foreach (string item in text)
        {
            Console.WriteLine(item);
        }
    }
}
