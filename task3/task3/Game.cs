using Spectre.Console;

namespace task3
{
    internal class Game()
    {
        private readonly List<Dice> dice;
        private readonly FairRandomGenerator randomGenerator;
        private readonly ProbabilityTableGenerator tableGenerator;

        public Game(List<Dice> dice) :this()
        {
            this.dice = dice;
            randomGenerator = new FairRandomGenerator();
            tableGenerator = new ProbabilityTableGenerator();
        }

        public void Run()
        {
            bool computerFirst = DetermineFirstMove();

            int computerDiceIndex, userDiceIndex;
            if (computerFirst)
            {
                computerDiceIndex = ComputerChooseDice(-1); 
                userDiceIndex = UserChooseDice(computerDiceIndex);
            }
            else
            {
                userDiceIndex = UserChooseDice(-1);
                computerDiceIndex = ComputerChooseDice(userDiceIndex);
            }

            int computerRoll = ComputerRoll(computerDiceIndex);
            int userRoll = UserRoll(userDiceIndex);

            if (userRoll > computerRoll)
            {
                Console.WriteLine($"You win ({userRoll} > {computerRoll})!");
            }
            else 
            { 
                Console.WriteLine($"You lose ({computerRoll} > {userRoll})!"); 
            }
        }

        private bool DetermineFirstMove()
        {
            Console.WriteLine("Let's determine who makes the first move.");
            var (key, computerNumber, hmac) = randomGenerator.GenerateNumber(1);
            Console.WriteLine($"I selected a random value in the range 0..1 (HMAC={hmac}).");
            Console.WriteLine("Try to guess my selection.");

            while (true)
            {
                DisplayOptions(new[] { "0", "1" });
                string input = Console.ReadLine()?.ToUpper();
                if (input == "X") Environment.Exit(0);
                if (input == "?")
                {
                    Console.WriteLine("Enter 0 or 1 to guess the computer's choice.");
                    continue;
                }
                if (int.TryParse(input, out int userNumber) && userNumber >= 0 && userNumber <= 1)
                {
                    int result = randomGenerator.GetFairResult(computerNumber, userNumber, 1);
                    Console.WriteLine($"My selection: {computerNumber} (KEY={Convert.ToHexString(key).ToUpper()}).");
                    bool computerFirst = Convert.ToBoolean(result);
                    Console.WriteLine(computerFirst ? "I make the first move." : "You make the first move.");
                    return computerFirst;
                }
                Console.WriteLine("Error: Enter 0 or 1.");
            }
        }

        private int ComputerChooseDice(int excludeIndex)
        {
            var available = Enumerable.Range(0, dice.Count).Where(i => i != excludeIndex).ToList();
            int choice = available[new Random().Next(available.Count)];
            Console.WriteLine($"I choose the {dice[choice]} dice.");
            return choice;
        }

        [Obsolete]
        private int UserChooseDice(int excludeIndex)
        {
            Console.WriteLine("Choose your dice:");
            while (true)
            {
                DisplayDice(excludeIndex);
                string input = Console.ReadLine()?.ToUpper();
                if (input == "X") Environment.Exit(0);
                if (input == "?")
                {
                    var table = tableGenerator.GenerateTable(dice);
                    AnsiConsole.Render(table);
                    continue;
                }
                if (int.TryParse(input, out int index) && index >= 0 && index < dice.Count && index != excludeIndex)
                {
                    Console.WriteLine($"You choose the {dice[index]} dice.");
                    return index;
                }
                Console.WriteLine("Error: Choose a valid dice index.");
            }
        }

        private int ComputerRoll(int diceIndex)
        {
            Console.WriteLine("It's time for my roll.");
            var (key, computerNumber, hmac) = randomGenerator.GenerateNumber(5);
            Console.WriteLine($"I selected a random value in the range 0..5 (HMAC={hmac}).");
            Console.WriteLine("Add your number modulo 6.");

            int userNumber = GetUserNumber(5);
            int result = randomGenerator.GetFairResult(computerNumber, userNumber, 5);
            Console.WriteLine($"My number is {computerNumber} (KEY={Convert.ToHexString(key).ToUpper()}).");
            Console.WriteLine($"The fair number generation result is {computerNumber} + {userNumber} = {result} (mod 6).");
            int roll = dice[diceIndex].Roll(result);
            Console.WriteLine($"My roll result is {roll}.");
            return roll;
        }

        private int UserRoll(int diceIndex)
        {
            Console.WriteLine("It's time for your roll.");
            var (key, computerNumber, hmac) = randomGenerator.GenerateNumber(5);
            Console.WriteLine($"I selected a random value in the range 0..5 (HMAC={hmac}).");
            Console.WriteLine("Add your number modulo 6.");

            int userNumber = GetUserNumber(5);
            int result = randomGenerator.GetFairResult(computerNumber, userNumber, 5);
            Console.WriteLine($"My number is {computerNumber} (KEY={Convert.ToHexString(key).ToUpper()}).");
            Console.WriteLine($"The fair number generation result is {computerNumber} + {userNumber} = {result} (mod 6).");
            int roll = dice[diceIndex].Roll(result);
            Console.WriteLine($"Your roll result is {roll}.");
            return roll;
        }

        private int GetUserNumber(int max)
        {
            while (true)
            {
                DisplayOptions(Enumerable.Range(0, max + 1).Select(i => i.ToString()).ToArray());
                string input = Console.ReadLine()?.ToUpper();
                if (input == "X") Environment.Exit(0);
                if (input == "?")
                {
                    Console.WriteLine($"Enter a number from 0 to {max}.");
                    continue;
                }
                if (int.TryParse(input, out int number) && number >= 0 && number <= max)
                    return number;
                Console.WriteLine($"Error: Enter a number from 0 to {max}.");
            }
        }

        private void DisplayDice(int excludeIndex)
        {
            for (int i = 0; i < dice.Count; i++)
            {
                if (i != excludeIndex)
                    Console.WriteLine($"{i} - {dice[i]}");
            }
            Console.WriteLine("X - exit");
            Console.WriteLine("? - help");
            Console.Write("Your selection: ");
        }

        private void DisplayOptions(string[] options)
        {
            foreach (string option in options)
                Console.WriteLine($"{option} - {option}");
            Console.WriteLine("X - exit");
            Console.WriteLine("? - help");
            Console.Write("Your selection: ");
        }
    }
}
