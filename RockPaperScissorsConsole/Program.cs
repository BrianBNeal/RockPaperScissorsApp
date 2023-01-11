
#region RefactoredModularizedApp
//using RockPaperScissorsConsole;

//RPSGame game = new RPSGame();
//game.Run();
#endregion

#region ReactiveSpaghettiCode
// 'Reactive' code is a term describing code that was
// obviously written as a reaction to something (messy and unplanned).
// 'Spaghetti' code is another term describing such code because
// it's tangled together and hard to change one piece without affecting everything else.
// See below.

int playerScore = 0;
int computerScore = 0;
bool playing = true;

while (playing)
{
    //ask for user's choice
    string currentPlayerChoice = "";
    while (string.IsNullOrWhiteSpace(currentPlayerChoice))
    {
        string scoreBoard = $@"
 -----------------------------
 |  Player: {playerScore} | Computer: {computerScore}  |
 -----------------------------
";
        string options = @"
1. Rock
2. Paper
3. Scissors
";

        Console.Clear();
        Console.WriteLine(scoreBoard);
        Console.WriteLine(options);
        Console.Write(" What's your choice? ");

        string choiceInput = Console.ReadLine();

        if (choiceInput == "1" || choiceInput == "2" || choiceInput == "3")
        {
            currentPlayerChoice = choiceInput;
        }
    }

    //get computer's choice
    string currentComputerChoice = new Random().Next(1,4).ToString();

    //show the matchup graphics
    string rockGraphic = @"
    _______
---'   ____)
      (_____)
      (_____)
      (____)
---.__(___)
";
    string paperGraphic = @"
     _______
---'    ____)____
           ______)
          _______)
         _______)
---.__________)
";
    string scissorsGraphic = @"
     _______
---'    ____)____
           ______)
          _______)
         _______)
---.__________)
";

    string playerGraphic = currentPlayerChoice == "1" ? rockGraphic : currentPlayerChoice == "2" ? paperGraphic : currentPlayerChoice == "3" ? scissorsGraphic : "";
    string computerGraphic = currentComputerChoice == "1" ? rockGraphic : currentComputerChoice == "2" ? paperGraphic : currentComputerChoice == "3" ? scissorsGraphic : "";
    
    Console.WriteLine(@$" {playerGraphic} VS {computerGraphic}");

    //ask if continue and update scoreboard

    string result = "";
    if (currentPlayerChoice == "1") //rock
    {
        if (currentComputerChoice== "1")
        {
            result = "draw";
        }
        else if (currentComputerChoice== "2")
        {
            result = "loss";
        }
        else
        {
            result = "win";
        }
    }
    else if (currentPlayerChoice =="2") //paper
    {
        if (currentComputerChoice == "1")
        {
            result = "win";
        }
        else if (currentComputerChoice == "2")
        {
            result = "draw";
        }
        else
        {
            result = "loss";
        }
    }
    else //scissors
    {
        if (currentComputerChoice == "1")
        {
            result = "loss";
        }
        else if (currentComputerChoice == "2")
        {
            result = "win";
        }
        else
        {
            result = "draw";
        }
    }
   
    if (result == "win")
    {
        playerScore++;
    }
    else if (result == "loss")
    {
        computerScore++;
    }

    Console.WriteLine($"It was a {result}!");
    Console.Write("Would you like to play again (y/n)? ");
    playing = Console.ReadLine().ToLower() == "y";
}

#endregion
