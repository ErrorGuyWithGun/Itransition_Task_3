using task3;

class Program
{
    static void Main(string[] args)
    {
        InputHandle Value = new InputHandle(args);
        if (Value.InputDice != null) 
        { 
            Game game = new Game(Value.InputDice);
            game.Run();
        }

    }
}