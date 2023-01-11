namespace RockPaperScissorsConsole;

public class ScoreCard
{
    private int _wins;
    private int _losses;

    public ScoreCard()
    {
        _wins = 0;
        _losses = 0;
    }

    /// <summary>
    /// Updates the score of a game based on the result of the round.
    /// </summary>
    /// <param name="result">The result of the round being played.</param>
    public void RecordResult(RPSResult result)
    {
        switch (result)
        {
            case RPSResult.Win:
                _wins++;
                break;
            case RPSResult.Loss:
                _losses++;
                break;
            default:
                break;
        }
    }

    public override string ToString()
    {

        return $@"
 -----------------------------
 |  Player: {_wins} | Computer: {_losses}  |
 -----------------------------
";
    }
}
