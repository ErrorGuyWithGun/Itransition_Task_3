namespace task3
{
    internal class InputHandle 
    {
        private List<Dice> _inputDice;
        public List<Dice> InputDice { get { return _inputDice; } }
        public InputHandle(string[] args) 
        {
            this._inputDice = null;
            List<Dice> diceList = new List<Dice>();

            if (args.Length < 3)
            {
                Console.WriteLine("ERROR:You passed less than three sets of digits.");
                return;
            }
            else
            {
               
                for (int i = 0; i < args.Length; i++)
                {
                    string[] numbers = args[i].Split(',');

                    List<int> faces = new List<int>();
                    foreach (var num in numbers)
                    {
                        if (!int.TryParse(num, out int number))
                        {
                            Console.WriteLine($"ERROR:Incorrect number entered: {num} in set {i+1}");
                            return;
                        }
                        if (Convert.ToInt32(num) <= 0) 
                        {
                            Console.WriteLine($"ERROR:Face cannot be negative: {num} in set {i + 1}");
                            return;
                        }
                        faces.Add(number);
                    }
                    diceList.Add(new Dice(faces));

                    if (numbers.Length < 6 )
                    { 
                        Console.WriteLine($"ERROR: You passed less than six digits in set - {i+1} ");
                        return;
                    }
                    else if (numbers.Length > 6) 
                    {
                        Console.WriteLine($"ERROR: You passed more than six digits in set - {i + 1} ");
                    }
                    
                }
                this._inputDice = diceList; ;
            }
            
        }
    }
}
