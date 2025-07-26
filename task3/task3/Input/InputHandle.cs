
namespace task3.Input
{
    internal class InputHandle 
    {
        private List<Dice> _inputDice;
        public List<Dice> InputDice { get { return _inputDice; } }
        public InputHandle(string[] args) 
        {
            List<Dice> diceList = new List<Dice>();
            InputSetValidation(args.Length);
            InputFaceValidation(args, diceList);
        }

        private void InputSetValidation(int SetLength) {
            if (SetLength < 3)
                ErrorMessage("ERROR:You passed less than three sets of digits.");
        }
        private void InputFaceValidation(string[] args, List<Dice> diceList)
        {
            for (int i = 0; i < args.Length; i++)
            {
                string[] numbers = args[i].Split(',');
                List<int> faces = new List<int>();
                FaceTypeValidation(numbers, diceList, faces, i);
                FaceCountValidation(numbers.Length, i);
            }
            _inputDice = diceList;
        }
        private void FaceTypeValidation(string[] numbers, List<Dice> diceList, List<int> faces, int id) {
            foreach (var num in numbers){
                if (!int.TryParse(num, out int number) || Convert.ToInt32(num) <= 0)
                    ErrorMessage($"ERROR:Incorrect number entered: {num} in set {id + 1}");
                faces.Add(number);
            }
            diceList.Add(new Dice(faces));
        }
        private void FaceCountValidation(int Length, int id) {
            if (Length != 6)
                ErrorMessage($"ERROR: You need to passed six digits in set - {id + 1} ");
        }

        private void ErrorMessage(string mes)
        {
            Console.WriteLine(mes);
            Environment.Exit(0);
        }
    }
}
